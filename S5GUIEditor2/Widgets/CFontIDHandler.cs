using System.ComponentModel;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CFontIDHandler : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    internal required ImageCache Cache { get; init; }
    
    internal string FontName { 
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(FontName));
            OnPropertyChanged(nameof(Font));
        }
    } = @"data\menu\fonts\standard12.met";

    internal static CFontIDHandler FromXml(XElement? e, ImageCache c)
    {
        return new CFontIDHandler()
        {
            FontName = e?.Element("FontName")?.Value ?? "",
            Cache = c
        };
    }

    public XElement ToXml()
    {
        return new XElement("FontName", FontName);
    }
    
    internal RWFont? Font => Cache.GetFont(FontName);
}