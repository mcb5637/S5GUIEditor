using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using MsBox.Avalonia;
using S5GUIEditor2.Widgets;

namespace S5GUIEditor2;

public partial class MaterialControl : UserControl
{
    public MaterialControl()
    {
        InitializeComponent();
    }

    private static readonly List<FilePickerFileType> FileTypes = [
        new("s5 textures")
        {
            Patterns = ["*.png", "*.dds"],
        },
        FilePickerFileTypes.All,
    ];
    
    private async void SelectTexture(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (TopLevel.GetTopLevel(this) is not MainWindow window)
                return;
            var storage = window.StorageProvider;
            var r = await storage.OpenFilePickerAsync(new()
            {
                Title = "Select Texture",
                SuggestedStartLocation = await storage.TryGetFolderFromPathAsync(Path.Combine(window.Settings.WorkspacePath, "graphics/textures/gui")),
                FileTypeFilter = FileTypes,
            });
            if (r.Count == 0)
                return;
            (DataContext as CMaterial)?.Texture = window.Settings.ToS5Path(r[0].Path.AbsolutePath);
        }
        catch (Exception ex)
        {
            Debugger.Break();
            await MessageBoxManager.GetMessageBoxStandard("exception", ex.ToString()).ShowAsync();
        }
    }
}