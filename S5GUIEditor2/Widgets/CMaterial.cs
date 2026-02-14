using System.ComponentModel;
using System.Xml.Linq;
using Avalonia.Media;
using Color = Avalonia.Media.Color;

namespace S5GUIEditor2.Widgets;

internal class CMaterial : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    internal void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal string Texture {
        get;
        set 
        {
            field = value;
            OnPropertyChanged(nameof(Texture));
            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(GridWidth));
            OnPropertyChanged(nameof(GridHeight));
            OnPropertyChanged(nameof(GridX));
            OnPropertyChanged(nameof(GridY));
        } 
    } = "";

    private void OnCoordinateChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Image));
        OnPropertyChanged(nameof(GridWidth));
        OnPropertyChanged(nameof(GridHeight));
        OnPropertyChanged(nameof(GridX));
        OnPropertyChanged(nameof(GridY));
        OnPropertyChanged(nameof(TextureCoordinates));
    }
    internal RectangleF TextureCoordinates
    {
        get;
        set
        {
            field.PropertyChanged -= OnCoordinateChanged;
            field = value;
            field.PropertyChanged += OnCoordinateChanged;
            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(GridWidth));
            OnPropertyChanged(nameof(GridHeight));
            OnPropertyChanged(nameof(GridX));
            OnPropertyChanged(nameof(GridY));
            OnPropertyChanged(nameof(TextureCoordinates));
        }
    } = new();

    internal Color Color
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Color));
        }
    }
    internal required ImageCache Cache { get; init; }
    
    internal IImage? Image => Cache.Get(Texture);

    internal double GridWidth
    {
        get => Image == null ? 0 : TextureCoordinates.Width * Image.Size.Width;
        set
        {
            if (Image == null)
                return;
            TextureCoordinates.Width = value / Image.Size.Width;
        }
    }
    internal double GridHeight
    {
        get => Image == null ? 0 : TextureCoordinates.Height * Image.Size.Height;
        set
        {
            if (Image == null)
                return;
            TextureCoordinates.Height = value / Image.Size.Height;
        }
    }
    internal double GridX
    {
        get => Image == null ? 0 : TextureCoordinates.X * Image.Size.Width / GridWidth;
        set
        {
            if (Image == null)
                return;
            TextureCoordinates.X = value / Image.Size.Width * GridWidth;
        }
    }
    internal double GridY
    {
        get => Image == null ? 0 : TextureCoordinates.Y * Image.Size.Height / GridHeight;
        set
        {
            if (Image == null)
                return;
            TextureCoordinates.Y = value / Image.Size.Height * GridHeight;
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