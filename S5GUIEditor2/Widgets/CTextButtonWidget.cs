using System.Collections.Generic;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CTextButtonWidget : CButtonWidget
{
    internal const string ClassName = "EGUIX::CTextButtonWidget";
    internal const uint ClassId = 0x5DD085A6;

    internal CTextButtonWidget(ImageCache c) : base(c)
    {
        StringHelper = new()
        {
            Font = new()
            {
                Cache = c,
            }
        };
    }

    private CWidgetStringHelper StringHelper { get; set; }
    internal bool CppLogicCenterText { get; set; } = true;
    private UpdateFunc Update { get; set; } = new();

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        StringHelper = CWidgetStringHelper.FromXml(e?.Element("StringHelper"), c);
        CppLogicCenterText = e?.Element("CenterText")?.Value.TryParseBool() ?? true;
        Update = UpdateFunc.FromXml(e);
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("StringHelper", StringHelper.ToXml()));
        e.Add(new XElement("CenterText", CppLogicCenterText.ToString().ToLower()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(Update.ToXml());
        return e;
    }

    protected override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateTextButtonWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(string before)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(before);
        s += StringHelper.ToLua(escapedname);
        s += $"CppLogic.UI.TextButtonSetCenterText(\"{Name}\", {CppLogicCenterText.ToString().ToLower()})\n";
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
    internal override CWidgetStringHelper TextRender => StringHelper;
    
    internal override IEnumerable<string> ReferencedFiles => [
        StringHelper.Font.FontName,
        MaterialsNormal.Texture,
        MaterialsHover.Texture,
        MaterialsPressed.Texture,
        MaterialsDisabled.Texture,
        MaterialsHighlighted.Texture,
    ];
}