using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CPureTooltipWidget : CBaseWidget
{
    internal const string ClassName = "EGUIX::CPureTooltipWidget";
    internal const uint ClassId = 0x82CC8876;
    private CToolTipHelper ToolTipHelper { get; set; } = new();

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        ToolTipHelper = CToolTipHelper.FromXml(e?.Element("ToolTipHelper"));
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("ToolTipHelper", ToolTipHelper.ToXml()));
        return e;
    }

    protected override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreatePureTooltipWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(ignorebef);
        s += ToolTipHelper.ToLua(escapedname);
        return s;
    }

    internal override CToolTipHelper Tooltip => ToolTipHelper;
}