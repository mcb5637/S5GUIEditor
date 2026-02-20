using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using bbaLib;
using MsBox.Avalonia;
using S5GUIEditor2.Widgets;

namespace S5GUIEditor2;

internal partial class MainWindow : Window
{
    private readonly DataFormat<string> DragDropFormat = DataFormat.CreateStringApplicationFormat("xxx-avalonia-controlcatalog-custom");
    
    public MainWindow()
    {
        M = new Model();
        C = new ImageCache() { S = M.Settings };
        InitializeComponent();
        Menu_New(null, null);
        DataContext = M;
        Menu_LoadXml(null, null);
    }

    private readonly Model M;
    private readonly ImageCache C;
    internal Settings Settings => M.Settings;

    private void LoadXml(string xmlPath)
    {
        XDocument xd = XDocument.Load(xmlPath);
        CProjectWidget w = new();
        w.FromXml(xd.Root, C);
        M.CurrentWidget.Clear();
        M.CurrentWidget.Add(w);
        M.OnPropertyChanged(nameof(M.RootWidget));
        w.PropertyChanged += (_, _) => Renderer.InvalidateVisual();
    }

    private void Menu_LoadXml(object? sender, RoutedEventArgs? e)
    {
        string p = Path.Combine(M.Settings.WorkspacePath, "menu/projects/ingame.xml");
        if (File.Exists(p))
            LoadXml(p);
    }

    private void Menu_Save(object? sender, RoutedEventArgs e)
    {
        XDocument xd = new XDocument();
        xd.Add(M.CurrentWidget.FirstOrDefault()?.ToXml());
        xd.Save("/home/mcb/Games/s5/drive_c/dedk/merged_e2_bbas/menu/projects/ingame_out.xml");
    }

    private void Menu_New(object? sender, RoutedEventArgs? e)
    {
        M.CurrentWidget.Clear();
        CProjectWidget w = new()
        {
            Name = "GUIRoot",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 1024,
                Height = 768,
            },
        };
        w.WidgetListHandler.SubWidgets.Add(new CContainerWidget()
        {
            Name = "Root",
            PositionAndSize = w.PositionAndSize,
        });
        
        M.CurrentWidget.Add(w);
    }

    private async void Menu_SetWorkspace(object? sender, RoutedEventArgs e)
    {
        try
        {
            var r = await StorageProvider.OpenFolderPickerAsync(new()
            {
                Title = "Select Workspace",
            });
            if (r.Count == 0)
                return;
            M.Settings.WorkspacePath = r[0].Path.AbsolutePath;
            M.StoreSettings();
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    private async void Menu_UnpackS5Data(object? sender, RoutedEventArgs e)
    {
        try
        {
            string? s5Path = null;
            if (s5Path == null)
            {
                var r = await StorageProvider.OpenFolderPickerAsync(new()
                {
                    Title = "Select S5 installation directory",
                });
                if (r.Count == 0)
                    return;
                s5Path = r[0].Path.AbsolutePath;
            }

            using BbaArchive a = new();
            // gold
            var add = (string s) => s.StartsWith("graphics\\textures\\gui") || s.StartsWith("menu") || s.StartsWith("text");
            foreach (string s in new[]{"base/data.bba", "extra2/bba/patch.bba", "extra2/bba/data.bba", "extra2/bba/patche2.bba"})
            {
                string p = Path.Combine(s5Path, s);
                if (!File.Exists(p))
                    continue;
                a.ReadBba(p, add);
            }
            a.WriteToFolder(M.Settings.WorkspacePath, null, _ => true);
            
            if (Directory.Exists(Path.Combine(s5Path, "base/shr")))
            {
                // history edition
                foreach (string source in new[] { "base/shr/", "extra2/shr/" })
                {
                    foreach (string folder in new[] { "graphics/textures/gui", "menu", "text" })
                    {
                        string sourceFolder = Path.Combine(s5Path, source, folder);
                        string dstFolder = Path.Combine(M.Settings.WorkspacePath, folder);
                        if (Directory.Exists(sourceFolder))
                            CopyDirRecursive(sourceFolder, dstFolder, []);
                    }
                }
            }
            
            await MessageBoxManager.GetMessageBoxStandard("done", "done").ShowAsync();
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    private static void CopyDirRecursive(string sourceDirectory, string targetDirectory, IEnumerable<string> exclude)
    {
        DirectoryInfo diSource = new(sourceDirectory);
        DirectoryInfo diTarget = new(targetDirectory);

        CopyDirRecursive(diSource, diTarget, exclude);
    }

    private static void CopyDirRecursive(DirectoryInfo source, DirectoryInfo target, IEnumerable<string> exclude)
    {
        Directory.CreateDirectory(target.FullName);

        foreach (FileInfo fi in source.GetFiles())
        {
            // ReSharper disable once PossibleMultipleEnumeration
            if (exclude.Contains(fi.Name))
                continue;
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            // ReSharper disable once PossibleMultipleEnumeration
            if (exclude.Contains(diSourceSubDir.Name))
                continue;
            DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
            // ReSharper disable once PossibleMultipleEnumeration
            CopyDirRecursive(diSourceSubDir, nextTargetSubDir, exclude);
        }
    }
    
    // ReSharper disable once AsyncVoidEventHandlerMethod
    private async void Widget_Copy(object? sender, RoutedEventArgs e)
    {
        await WidgetCopy();
    }

    internal async Task WidgetCopy()
    {
        try
        {
            if ((M.SelectedWidgets?.Count ?? 0) == 0)
                return;
            var s = M.SelectedWidgets?.Select(x => x.ToXml().ToString()).Aggregate((a, b) => a + "\0" + b);
            var c = Clipboard;
            if (c == null || s == null)
                return;
            await c.SetTextAsync(s);
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    // ReSharper disable once AsyncVoidEventHandlerMethod
    private async void Widget_Paste(object? sender, RoutedEventArgs e)
    {
        await WidgetPaste();
    }

    internal async Task WidgetPaste()
    {
        try
        {
            var parent = M.EditWidget as CContainerWidget;
            var c = Clipboard;
            if (c == null || parent == null)
                return;
            var s = await c.TryGetTextAsync();
            if (s == null)
                return;
            foreach (var w in s.Split('\0').Where(x => !string.IsNullOrWhiteSpace(x)).
                         Select(x => CBaseWidget.GetFromXml(XDocument.Parse(x).Element("WidgetList"), C)))
                parent.WidgetListHandler.SubWidgets.Add(w);
            Renderer.InvalidateVisual();
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    private async void Widget_Delete(object? sender, RoutedEventArgs e)
    {
        try {
            if (M.SelectedWidgets == null || M.SelectedWidgets.Count == 0)
                return;
            CBaseWidget? firstParent = null;
            foreach (var w in M.SelectedWidgets.ToArray())
            {
                if (w.ParentNode == null)
                    return;
                firstParent ??= w.ParentNode;
                w.ParentNode.WidgetListHandler.SubWidgets.Remove(w);
                w.ParentNode = null;
            }

            M.SelectedWidgets.Clear();
            if (firstParent != null)
                M.SelectedWidgets.Add(firstParent);
            Renderer.InvalidateVisual();
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    private void Widget_NewContainer(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CContainerWidget()
        {
            Name = "New Container",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }
    private void Widget_NewStatic(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CStaticWidget(C)
        {
            Name = "New",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }
    private void Widget_NewStaticText(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CStaticTextWidget(C)
        {
            Name = "New",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }
    private void Widget_NewPureTooltip(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CPureTooltipWidget()
        {
            Name = "New",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }
    private void Widget_NewProgressBar(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CProgressBarWidget(C)
        {
            Name = "New",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }
    private void Widget_NewGfxButton(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CGfxButtonWidget(C)
        {
            Name = "New",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }
    private void Widget_NewTextButton(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CTextButtonWidget(C)
        {
            Name = "New",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }
    private void Widget_NewCustomWidget(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget parent)
            return;
        parent.WidgetListHandler.SubWidgets.Add(new CCustomWidget()
        {
            Name = "New",
            PositionAndSize = new RectangleF()
            {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100,
            },
            ParentNode = parent,
        });
    }

    private async void Widget_ExportLua(object? sender, RoutedEventArgs e)
    {
        try
        {
            var l = M.SelectedWidgets;
            if (l == null || l.Count == 0 || Clipboard == null)
                return;
            await Clipboard.SetTextAsync(CBaseWidget.GetLua(l));
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    private void Widget_SortChildren(object? sender, RoutedEventArgs e)
    {
        if (M.EditWidget is not CContainerWidget w)
            return;
        var a = w.WidgetListHandler.SubWidgets.OrderByDescending(x => x.ZPriority).ToArray();
        w.WidgetListHandler.SubWidgets.Clear();
        foreach (var x in a)
            w.WidgetListHandler.SubWidgets.Add(x);
        Renderer.InvalidateVisual();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        M.OnSelectionChanged();
        Renderer.InvalidateVisual();
    }

    private async void TreeDragStart(object? sender, PointerPressedEventArgs e)
    {
        try
        {
            if (e.Properties.IsRightButtonPressed)
                return;
            if (sender is not Control cnt)
                return;
            if (cnt.DataContext is not CBaseWidget wid)
                return;
            var dragData = new DataTransfer();
            dragData.Add(DataTransferItem.Create(DragDropFormat, wid.Id));
            await DragDrop.DoDragDropAsync(e, dragData, DragDropEffects.Move);
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    // ReSharper disable once UnusedMember.Local
    private void TreeDragOver(object? sender, DragEventArgs e)
    {
        var (target, _, move) = CheckDropTarget(sender, e);
        if (target == null || move == null)
            e.DragEffects = DragDropEffects.None;
        else
            e.DragEffects &= DragDropEffects.Move;
    }

    // ReSharper disable once UnusedMember.Local
    private async void TreeDrop(object? sender, DragEventArgs e)
    {
        try
        {
            var (target, pos, move) = CheckDropTarget(sender, e);
            if (target == null || move == null)
                return;
            
            move.ParentNode!.WidgetListHandler.SubWidgets.Remove(move);
            if (pos >= 0)
                target.WidgetListHandler.SubWidgets.Insert(pos, move);
            else
                target.WidgetListHandler.SubWidgets.Add(move);
            move.ParentNode = target;
            Renderer.InvalidateVisual();
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    private (CContainerWidget?, int, CBaseWidget?) CheckDropTarget(object? sender, DragEventArgs e)
    {
        if (sender is not Control cnt)
            return (null, 0, null);
        if (cnt.DataContext is not CBaseWidget droppedOn)
            return (null, 0, null);
        string r = e.DataTransfer.TryGetValue(DragDropFormat) ?? "";
        CBaseWidget? move = M.GetById(r);
        if (move == null || droppedOn == move || move.ParentNode == null)
            return (null, 0, null);
        CContainerWidget target;
        CBaseWidget? before = null;
        if (e.KeyModifiers.HasFlag(KeyModifiers.Control))
        {
            if (droppedOn is not CContainerWidget cw)
                return (null, 0, null);
            target = cw;
        }
        else
        {
            if (droppedOn.ParentNode == null)
                return (null, 0, null);
            target = droppedOn.ParentNode;
            before = droppedOn;
        }
        if (move is CContainerWidget cc && cc.IsChildRecursive(target) || move == target)
            return (null, 0, null);
        var idx = before == null ? -1 : target.WidgetListHandler.SubWidgets.IndexOf(before);
        return (target, idx, move);
    }

    private static readonly List<FilePickerFileType> FileTypes = [
        new("s5 fonts")
        {
            Patterns = ["*.met"],
        },
        FilePickerFileTypes.All,
    ];
    private async void SelectFont(object? sender, RoutedEventArgs e)
    {
        try
        {
            var storage = StorageProvider;
            var r = await storage.OpenFilePickerAsync(new()
            {
                Title = "Select Font",
                SuggestedStartLocation = await storage.TryGetFolderFromPathAsync(M.Settings.WorkspacePath),
                FileTypeFilter = FileTypes,
            });
            if (r.Count == 0)
                return;
            M.EditText?.Font.FontName = M.Settings.ToS5Path(r[0].Path.AbsolutePath);
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }
}