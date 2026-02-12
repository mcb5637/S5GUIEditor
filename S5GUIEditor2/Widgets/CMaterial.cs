using System.Drawing;
using System.Xml.Linq;
using Color = Avalonia.Media.Color;

namespace S5GUIEditor2.Widgets;

internal class CMaterial
{
    internal string Texture { get; set; } = "";
    internal RectangleF TextureCoordinates { get; set; }
    internal Color Color { get; set; }


    internal static CMaterial FromXml(XElement? e)
    {
        return new CMaterial
        {
            Texture = e?.Element("Texture")?.Value ?? "",
            TextureCoordinates = RectangleF.FromXml(e?.Element("TextureCoordinates")),
            Color = Color.FromXml(e?.Element("Color")),
        };
    }

    public XElement[] ToXml()
    {
        return
        [
            new XElement("Texture", Texture),
            // ReSharper disable once CoVariantArrayConversion
            new XElement("TextureCoordinates", TextureCoordinates.ToXml()),
            // ReSharper disable once CoVariantArrayConversion
            new XElement("Color", Color.ToXml()),
        ];
    }
    
    public string ToLua(string escapedname, int i)
    {
        RectangleF r = TextureCoordinates;
        string s = $"CppLogic.UI.WidgetMaterialSetTextureCoordinates({escapedname}, {i}, {r.X}, {r.Y}, {r.Width}, {r.Height})\n";
        if (Texture.Length > 0)
            s += $"XGUIEng.SetMaterialTexture({escapedname}, {i}, \"{Texture.Replace("\\", @"\\")}\")\n";
        s += $"XGUIEng.SetMaterialColor({escapedname}, {i}, {Color.R}, {Color.G}, {Color.B}, {Color.A})\n";
        return s;
    }
}