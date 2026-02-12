using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using S5GUIEditor2.Widgets;

namespace S5GUIEditor2;

internal class Model : INotifyPropertyChanged
{
    
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
}