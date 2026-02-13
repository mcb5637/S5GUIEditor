using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using S5GUIEditor2.Widgets;

namespace S5GUIEditor2;

internal partial class MainWindow : Window
{
    public MainWindow()
    {
        M = new Model();
        ImageCache.Instance = new ImageCache() { S = M.Settings };
        InitializeComponent();
        Menu_New(null, null);
        DataContext = M;
        LoadXml("/home/mcb/Games/s5/drive_c/dedk/merged_e2_bbas/menu/projects/ingame.xml");
    }

    private readonly Model M;

    private void LoadXml(string xmlPath)
    {
        XDocument xd = XDocument.Load(xmlPath);
        CProjectWidget w = new();
        w.FromXml(xd.Root);
        M.CurrentWidget.Clear();
        M.CurrentWidget.Add(w);
    }

    private void Menu_LoadXml(object? sender, RoutedEventArgs e)
    {
        LoadXml("/home/mcb/Games/s5/drive_c/dedk/merged_e2_bbas/menu/projects/ingame.xml");
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
            PositionAndSize = new RectangleF(0, 0, 1024, 768),
        };
        w.WidgetListHandler.SubWidgets.Add(new CContainerWidget()
        {
            Name = "Root",
            PositionAndSize = w.PositionAndSize,
        });
        
        M.CurrentWidget.Add(w);
    }

    private async void Widget_Copy(object? sender, RoutedEventArgs e)
    {
        try
        {
            var s = M.SelectedWidgets?.FirstOrDefault()?.ToXml().ToString();
            var c = Clipboard;
            if (c == null)
                return;
            await c.SetTextAsync(s);
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        M.OnSelectionChanged();
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
}