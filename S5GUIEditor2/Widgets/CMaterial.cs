using System.Drawing;
using System.Xml.Linq;
using Avalonia.Media;
using Color = Avalonia.Media.Color;

namespace S5GUIEditor2.Widgets;

internal class CMaterial
{
    internal string Texture { get; set; } = "";
    internal RectangleF TextureCoordinates { get; set; }
    internal Color Color { get; set; }
    internal required ImageCache Cache { get; init; }
    
    internal IImage? Image => Cache.Get(Texture);

    internal float GridSizeX
    {
        get => (float)(TextureCoordinates.Width * Image?.Size.Width ?? 0);
        set
        {
            if (Image == null)
                return;
            var d = TextureCoordinates;
            d.Width = (float)(value / Image.Size.Width);
            TextureCoordinates = d;
        }
    }
    internal float GridSizeY
    {
        get => (float)(TextureCoordinates.Height * Image?.Size.Height ?? 0);
        set
        {
            if (Image == null)
                return;
            var d = TextureCoordinates;
            d.Height = (float)(value / Image.Size.Height);
            TextureCoordinates = d;
        }
    }

    internal float GridIndexX
    {
        get => Image == null ? 0 : (float)(TextureCoordinates.X * Image.Size.Width / GridSizeX);
        set
        {
            if (Image == null)
                return;
            var d = TextureCoordinates;
            d.X = (float)(value / Image.Size.Width * GridSizeX);
            TextureCoordinates = d;
        }
    }
    internal float GridIndexY
    {
        get => Image == null ? 0 : (float)(TextureCoordinates.Y * Image.Size.Height / GridSizeY);
        set
        {
            if (Image == null)
                return;
            var d = TextureCoordinates;
            d.Y = (float)(value / Image.Size.Height * GridSizeY);
            TextureCoordinates = d;
        }
    }
    
    internal static CMaterial FromXml(XElement? e, ImageCache c)
    {
        return new CMaterial
        {
            Texture = e?.Element("Texture")?.Value ?? "",
            TextureCoordinates = RectangleF.FromXml(e?.Element("TextureCoordinates")),
            Color = Color.FromXml(e?.Element("Color")),
            Cache = c
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