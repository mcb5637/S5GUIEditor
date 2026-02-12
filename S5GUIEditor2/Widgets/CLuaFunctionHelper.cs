using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CLuaFunctionHelper
{
    internal string LuaCommand { get; set; } = "";

    internal static CLuaFunctionHelper FromXml(XElement? e)
    {
        return new CLuaFunctionHelper
        {
            LuaCommand = e?.Element("LuaCommand")?.Value ?? "",
        };
    }

    public XElement ToXml()
    {
        return new XElement("LuaCommand", LuaCommand);
    }
}