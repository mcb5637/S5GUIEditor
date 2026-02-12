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
        SelectedTabItem = 0;
        OnPropertyChanged(nameof(SelectedTabItem));
    }
}