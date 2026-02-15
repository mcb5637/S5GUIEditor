using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CTextButtonWidget : CButtonWidget
{
    internal const string ClassName = "EGUIX::CTextButtonWidget";
    internal const uint ClassId = 0x5DD085A6;

    internal CTextButtonWidget(ImageCache c) : base(c)
    {
    }

    private CWidgetStringHelper StringHelper { get; set; } = new();
    internal bool CppLogicCenterText { get; set; }
    private UpdateFunc Update { get; set; } = new();

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        StringHelper = CWidgetStringHelper.FromXml(e?.Element("StringHelper"));
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
    internal override string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(ignorebef);
        s += StringHelper.ToLua(escapedname);
        s += $"CppLogic.UI.TextButtonSetCenterText(\"{Name}\", {CppLogicCenterText.ToString().ToLower()})\n";
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
    internal override CWidgetStringHelper TextRender => StringHelper;
}