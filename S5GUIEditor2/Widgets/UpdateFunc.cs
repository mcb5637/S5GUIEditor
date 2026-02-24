using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

// not originally a struct, but these always appear together
internal class UpdateFunc
{
    internal CLuaFunctionHelper UpdateFunction { get; private init; } = new();
    internal bool UpdateManualFlag { get; set; } = true;

    internal static UpdateFunc FromXml(XElement? e)
    {
        return new UpdateFunc()
        {
            UpdateFunction = CLuaFunctionHelper.FromXml(e?.Element("UpdateFunction")),
            UpdateManualFlag = e?.Element("UpdateManualFlag")?.Value.TryParseBool() ?? false,
        };
    }

    public XElement[] ToXml()
    {
        return
        [
            new XElement("UpdateFunction", UpdateFunction.ToXml()),
            new XElement("UpdateManualFlag", UpdateManualFlag.ToString().ToLower()),
        ];
    }
    
    public string ToLua(string escapedname)
    {
        string s = $"CppLogic.UI.WidgetSetUpdateManualFlag({escapedname}, {UpdateManualFlag.ToString().ToLower()})\n";
        if (UpdateFunction.LuaCommand.Length > 0 && !UpdateFunction.LuaCommand.StartsWith("--"))
            s += $"CppLogic.UI.WidgetOverrideUpdateFunc({escapedname}, function() {UpdateFunction.LuaCommand} end)\n";
        return s;
    }
}