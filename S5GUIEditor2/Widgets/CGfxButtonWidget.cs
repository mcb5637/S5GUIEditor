using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CGfxButtonWidget : CButtonWidget
{
    internal new const string ClassName = "EGUIX::CGfxButtonWidget";
    internal new const uint ClassId = 0x56DDA656;

    internal CGfxButtonWidget(ImageCache c) : base(c)
    {
        IconMaterial = new CMaterial() { Cache = c };
    }

    internal CMaterial IconMaterial { get; set; }
    internal UpdateFunc Update { get; set; } = new();
    
    internal override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        IconMaterial = CMaterial.FromXml(e?.Element("IconMaterial"), c);
        Update = UpdateFunc.FromXml(e);
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("IconMaterial", IconMaterial.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(Update.ToXml());
        return e;
    }
    
    internal override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateGFXButtonWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(ignorebef);
        s += IconMaterial.ToLua(escapedname, 10);
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
    internal override CMaterial StaticMaterial => IconMaterial;
}