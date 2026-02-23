using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using S5GUIEditor2.Widgets;

namespace S5GUIEditor2;

internal class Model : INotifyPropertyChanged
{
    private const string SettingsJson = "settings.json";
    internal Settings Settings { get; }
    
    internal ObservableCollection<CBaseWidget> CurrentWidget { get; set; } = [];

    internal ObservableCollection<CBaseWidget>? SelectedWidgets { get; set; } = [];

    internal CBaseWidget? EditWidget => SelectedWidgets?.FirstOrDefault();
    internal bool HasEditWidget => EditWidget != null;
    
    internal int SelectedTabItem { get; set; } = 0;
    
    internal CToolTipHelper? EditTooltip => EditWidget?.Tooltip;
    internal bool HasEditTooltip => EditTooltip != null;

    internal CMaterial? EditStaticMaterial => EditWidget?.StaticMaterial;
    internal bool HasEditStaticMaterial => EditStaticMaterial != null;

    internal UpdateFunc? EditUpdate => EditWidget?.UpdateData;
    internal bool HasEditUpdate => EditUpdate != null;
    
    internal CProgressBarWidget? EditProgress => EditWidget as CProgressBarWidget;
    internal bool HasEditProgress => EditProgress != null;

    internal CWidgetStringHelper? EditText => EditWidget?.TextRender;
    internal bool HasEditText => EditText != null;
    
    internal CButtonWidget? EditButton => EditWidget as CButtonWidget;
    internal bool HasEditButton => EditButton != null;
    
    internal CStaticTextWidget? EditStaticText => EditWidget as CStaticTextWidget;
    internal bool HasEditStaticText => EditStaticText != null;

    internal CTextButtonWidget? EditTextButton => EditWidget as CTextButtonWidget;
    internal bool HasEditTextButton => EditTextButton != null;
    
    internal CCustomWidget? EditCustomWidget => EditWidget as CCustomWidget;
    internal bool HasEditCustomWidget => EditCustomWidget != null;
    
    internal CBaseWidget? RootWidget => CurrentWidget.FirstOrDefault();

    internal bool UIRenderDrag
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(UIRenderDrag));
        }
    } = false;

    public event PropertyChangedEventHandler? PropertyChanged;

    internal void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal void OnSelectionChanged()
    {
        OnPropertyChanged(nameof(EditWidget));
        OnPropertyChanged(nameof(HasEditWidget));
        OnPropertyChanged(nameof(EditTooltip));
        OnPropertyChanged(nameof(HasEditTooltip));
        OnPropertyChanged(nameof(EditStaticMaterial));
        OnPropertyChanged(nameof(HasEditStaticMaterial));
        OnPropertyChanged(nameof(EditUpdate));
        OnPropertyChanged(nameof(HasEditUpdate));
        OnPropertyChanged(nameof(EditProgress));
        OnPropertyChanged(nameof(HasEditProgress));
        OnPropertyChanged(nameof(EditText));
        OnPropertyChanged(nameof(HasEditText));
        OnPropertyChanged(nameof(EditButton));
        OnPropertyChanged(nameof(HasEditButton));
        OnPropertyChanged(nameof(EditStaticText));
        OnPropertyChanged(nameof(HasEditStaticText));
        OnPropertyChanged(nameof(EditTextButton));
        OnPropertyChanged(nameof(HasEditTextButton));
        OnPropertyChanged(nameof(EditCustomWidget));
        OnPropertyChanged(nameof(HasEditCustomWidget));
        SelectedTabItem = 0;
        OnPropertyChanged(nameof(SelectedTabItem));
    }

    internal void StoreSettings()
    {
        var s = JsonSerializer.Serialize(Settings, SourceGenerationContext.Default.Settings);
        File.WriteAllText(SettingsJson, s);
    }

    internal Model()
    {
        Settings = ReadSettings(ExistingFileList);
    }

    private static Settings ReadSettings(Func<IEnumerable<string>> l)
    {
        Settings r;
        try
        {
            var s = File.ReadAllText(SettingsJson);
            r = JsonSerializer.Deserialize<Settings>(s, SourceGenerationContext.Default.Settings) ?? new Settings();
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            r = new Settings();
        }
        r.ExistingFileList = l;
        return r;
    }

    internal CBaseWidget? GetById(string id)
    {
        return Search(CurrentWidget);

        CBaseWidget? Search(IEnumerable<CBaseWidget> w)
        {
            foreach (CBaseWidget c in w)
            {
                if (c.Id == id)
                    return c;
                if (c is CContainerWidget cw)
                {
                    var r = Search(cw.WidgetListHandler.SubWidgets);
                    if (r != null)
                        return r;
                }
            }
            return null;
        }
    }

    private IEnumerable<string> ExistingFileList()
    {
        return CurrentWidget.SelectMany(x => x.ReferencedFiles);
    }
}