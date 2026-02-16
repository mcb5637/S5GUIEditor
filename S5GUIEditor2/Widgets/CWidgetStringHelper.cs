using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Avalonia.Media;

namespace S5GUIEditor2.Widgets;

internal class CWidgetStringHelper : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal required CFontIDHandler Font { get; init; }
    internal CSingleStringHandler String { get; private init; } = new();
    internal float StringFrameDistance { get; set; }

    internal Color Color
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Color));
        }
    } = Colors.White;
    
    internal static CWidgetStringHelper FromXml(XElement? e, ImageCache c)
    {
        return new CWidgetStringHelper()
        {
            Font = CFontIDHandler.FromXml(e?.Element("Font"), c),
            String = CSingleStringHandler.FromXml(e?.Element("String")),
            StringFrameDistance = e?.Element("StringFrameDistance")?.Value.TryParseFloat() ?? 0.0f,
            Color = Color.FromXml(e?.Element("Color")),
        };
    }
    
    public XElement[] ToXml()
    {
        return
        [
            new XElement("Font", Font.ToXml()),
            // ReSharper disable once CoVariantArrayConversion
            new XElement("String", String.ToXml()),
            new XElement("StringFrameDistance", StringFrameDistance.ToString(CultureInfo.InvariantCulture)),
            // ReSharper disable once CoVariantArrayConversion
            new XElement("Color", Color.ToXml())
        ];
    }
    public string ToLua(string escapedname)
    {
        string font = Font.FontName;
        if (!font.StartsWith("data"))
            font = "data\\" + font;
        font = font.Replace("\\", "\\\\");
        string s = $"CppLogic.UI.WidgetSetFont({escapedname}, \"{font}\")\n";
        s += $"CppLogic.UI.WidgetSetStringFrameDistance({escapedname}, {StringFrameDistance})\n";
        s += String.ToLua(escapedname);
        s += $"XGUIEng.SetTextColor({escapedname}, {Color.R}, {Color.G}, {Color.B}, {Color.A})\n";
        return s; 
    }
}