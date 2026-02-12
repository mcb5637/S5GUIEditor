using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CFontIDHandler
{
    internal string FontName { get; set; } = "";

    internal static CFontIDHandler FromXml(XElement? e)
    {
        return new CFontIDHandler()
        {
            FontName = e?.Attribute("FontName")?.Value ?? "",
        };
    }

    public XElement ToXml()
    {
        return new XElement("FontName", FontName);
    }
}