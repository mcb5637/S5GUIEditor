using System.Collections.Generic;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CGfxButtonWidget : CButtonWidget
{
    internal const string ClassName = "EGUIX::CGfxButtonWidget";
    internal const uint ClassId = 0x56DDA656;

    internal CGfxButtonWidget(ImageCache c) : base(c)
    {
        IconMaterial = new CMaterial() { Cache = c };
    }

    private CMaterial IconMaterial { get; set; }
    private UpdateFunc Update { get; set; } = new();

    protected override (string, uint) GetClass()
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

    protected override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateGFXButtonWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(string before)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(before);
        s += IconMaterial.ToLua(escapedname, 10);
        s += Update.ToLua(escapedname);
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
    internal override CMaterial StaticMaterial => IconMaterial;
    
    internal override IEnumerable<string> ReferencedFiles => [
        MaterialsNormal.Texture,
        MaterialsHover.Texture,
        MaterialsPressed.Texture,
        MaterialsDisabled.Texture,
        MaterialsHighlighted.Texture,
        IconMaterial.Texture,
    ];
}