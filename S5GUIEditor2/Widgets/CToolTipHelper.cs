using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CToolTipHelper
{
    internal bool ToolTipEnabledFlag { get; set; }
    internal CSingleStringHandler ToolTipString { get; private init; } = new();
    internal string TargetWidget { get; set; } = "";
    internal bool ControlTargetWidgetDisplayState { get; set; }
    internal CLuaFunctionHelper UpdateFunction { get; private init; } = new();

    internal static CToolTipHelper FromXml(XElement? e)
    {
        return new CToolTipHelper()
        {
            ToolTipEnabledFlag = e?.Element("ToolTipEnabledFlag")?.Value.TryParseBool() ?? false,
            ToolTipString = CSingleStringHandler.FromXml(e?.Element("ToolTipString")),
            TargetWidget = e?.Element("TargetWidget")?.Value ?? "",
            ControlTargetWidgetDisplayState = e?.Element("ControlTargetWidgetDisplayState")?.Value.TryParseBool() ?? false,
            UpdateFunction = CLuaFunctionHelper.FromXml(e?.Element("UpdateFunction")),
        };
    }


    public XElement[] ToXml()
    {
        return
        [
            new XElement("ToolTipEnabledFlag", ToolTipEnabledFlag.ToString()),
            // ReSharper disable once CoVariantArrayConversion
            new XElement("ToolTipString", ToolTipString.ToXml()),
            new XElement("TargetWidget", TargetWidget),
            new XElement("ControlTargetWidgetDisplayState", ControlTargetWidgetDisplayState.ToString()),
            new XElement("UpdateFunction", UpdateFunction.ToXml())
        ];
    }
    
    public string ToLua(string escapedname)
    {
        string twid = TargetWidget.Length == 0 ? "nil" : $"\"{TargetWidget}\"";
        string s = $"CppLogic.UI.WidgetSetTooltipData({escapedname}, {twid}, {ControlTargetWidgetDisplayState.ToString().ToLower()}, {ToolTipEnabledFlag.ToString().ToLower()})\n";
        if (UpdateFunction.LuaCommand.Length > 0 && !UpdateFunction.LuaCommand.StartsWith("--"))
            s += $"CppLogic.UI.WidgetOverrideTooltipFunc({escapedname}, function() {UpdateFunction.LuaCommand} end)\n";
        if (ToolTipString.RawString.Length > 0)
            s += $"CppLogic.UI.WidgetSetTooltipString({escapedname}, \"{ToolTipString.RawString}\", false)\n";
        else if (ToolTipString.StringTableKey.Length > 0)
            s += $"CppLogic.UI.WidgetSetTooltipString({escapedname}, \"{ToolTipString.StringTableKey}\", true)\n";
        return s;
    }
}