using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Avalonia;

namespace S5GUIEditor2.Widgets;

internal class RectangleF : INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
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
            OnPropertyChanged(nameof(ToRect));
        }
    } = 0;
    internal double Y
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Y));
            OnPropertyChanged(nameof(ToRect));
        }
    } = 0;
    internal double Width
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(ToRect));
        }
    } = 0;
    internal double Height
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(ToRect));
        }
    } = 0;
    
    
    internal static RectangleF FromXml(XElement? e)
    {
        return new RectangleF
        {
            X = e?.Element("X")?.Value.TryParseFloat() ?? 0.0f,
            Y = e?.Element("Y")?.Value.TryParseFloat() ?? 0.0f,
            Width = e?.Element("W")?.Value.TryParseFloat() ?? 0.0f,
            Height = e?.Element("H")?.Value.TryParseFloat() ?? 0.0f
        };
    }

    internal XElement[] ToXml()
    {
        return
        [
            new XElement("X", X.ToString(CultureInfo.InvariantCulture)),
            new XElement("Y", Y.ToString(CultureInfo.InvariantCulture)),
            new XElement("W", Width.ToString(CultureInfo.InvariantCulture)),
            new XElement("H", Height.ToString(CultureInfo.InvariantCulture))
        ];
    }
    
    internal Rect ToRect => new Rect(X, Y, Width, Height);
}