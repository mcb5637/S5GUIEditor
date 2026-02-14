using System.ComponentModel;

namespace S5GUIEditor2.Widgets;

public class RectangleF : INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler? PropertyChanged;

    internal void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal double X
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(X));
        }
    } = 0;
    internal double Y
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Y));
        }
    } = 0;
    internal double Width
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Width));
        }
    } = 0;
    internal double Height
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Height));
        }
    } = 0;
}