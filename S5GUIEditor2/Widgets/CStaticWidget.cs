using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CStaticWidget : CBaseWidget
{
    internal new const string ClassName = "EGUIX::CStaticWidget";
    internal new const uint ClassId = 0x213A8776;
    internal CMaterial BackgroundMaterial { get; set; }

    internal CStaticWidget(ImageCache c)
    {
        BackgroundMaterial = new CMaterial() { Cache = c };
    }
    
    internal override (string, uint) GetClass()
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
    
    internal override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateStaticWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(ignorebef);
        s += BackgroundMaterial.ToLua(escapedname, 0);
        return s;
    }

    internal override CMaterial StaticMaterial => BackgroundMaterial;
}