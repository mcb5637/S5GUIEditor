using Color = Avalonia.Media.Color;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal static class ExtensionsColor
{
    extension(string? s)
    {
        internal float? TryParseFloat()
        {
            return float.TryParse(s, out var result) ? result : null;
        }

        internal byte? TryParseByte()
        {
            return byte.TryParse(s, out var result) ? result : null;
        }

        internal bool? TryParseBool()
        {
            return bool.TryParse(s, out var result) ? result : null;
        }

        internal int? TryParseInt()
        {
            return int.TryParse(s, out var result) ? result : null;
        }
    }

    extension(Color color)
    {
        internal static Color FromXml(XElement? e)
        {
            var r = e?.Element("Red")?.Value.TryParseByte() ?? 0;
            var g = e?.Element("Green")?.Value.TryParseByte() ?? 0;
            var b = e?.Element("Blue")?.Value.TryParseByte() ?? 0;
            var a = e?.Element("Alpha")?.Value.TryParseByte() ?? 0;
            return Color.FromArgb(a, r, g, b);
        }

        internal XElement[] ToXml()
        {
            return
            [
                new XElement("Red", color.R.ToString()),
                new XElement("Green", color.G.ToString()),
                new XElement("Blue", color.B.ToString()),
                new XElement("Alpha", color.A.ToString())
            ];
        }
    }
}