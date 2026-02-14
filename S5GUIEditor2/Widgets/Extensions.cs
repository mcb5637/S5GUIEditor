using System.Globalization;
using Color = Avalonia.Media.Color;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal static class ExtensionsRect
{
    internal static float? TryParseFloat(this string? s)
    {
        return float.TryParse(s, out var result) ? result : null;
    }

    internal static byte? TryParseByte(this string? s)
    {
        return byte.TryParse(s, out var result) ? result : null;
    }
    internal static bool? TryParseBool(this string? s)
    {
        return bool.TryParse(s, out var result) ? result : null;
    }
    internal static int? TryParseInt(this string? s)
    {
        return int.TryParse(s, out var result) ? result : null;
    }

    extension(RectangleF rect)
    {
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
                new XElement("X", rect.X.ToString(CultureInfo.InvariantCulture)),
                new XElement("Y", rect.Y.ToString(CultureInfo.InvariantCulture)),
                new XElement("W", rect.Width.ToString(CultureInfo.InvariantCulture)),
                new XElement("H", rect.Height.ToString(CultureInfo.InvariantCulture))
            ];
        }
    }
}

internal static class ExtensionsColor
{
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