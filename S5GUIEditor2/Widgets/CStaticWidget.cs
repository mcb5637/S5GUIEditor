using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CStaticWidget : CBaseWidget
{
    internal const string ClassName = "EGUIX::CStaticWidget";
    internal const uint ClassId = 0x213A8776;

    internal CMaterial BackgroundMaterial
    {
        get;
        set
        {
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            field?.PropertyChanged -= PropertyChangedHandlerMaterial;
            field = value;
            field.PropertyChanged += PropertyChangedHandlerMaterial;
            OnPropertyChanged(nameof(BackgroundMaterial));
            OnPropertyChanged(nameof(RendererMaterial));
        }
    }

    internal CStaticWidget(ImageCache c)
    {
        BackgroundMaterial = new CMaterial() { Cache = c };
    }

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        BackgroundMaterial = CMaterial.FromXml(e?.Element("BackgroundMaterial"), c);
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("BackgroundMaterial", BackgroundMaterial.ToXml()));
        return e;
    }

    protected override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateStaticWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(string before)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(before);
        s += BackgroundMaterial.ToLua(escapedname, 0);
        return s;
    }

    internal override CMaterial StaticMaterial => BackgroundMaterial;
    internal override CMaterial RendererMaterial => BackgroundMaterial;
}