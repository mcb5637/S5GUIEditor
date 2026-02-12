using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CSingleStringHandler
{
    internal string StringTableKey { get; set; } = "";
    internal string RawString { get; set; } = "";

    internal static CSingleStringHandler FromXml(XElement? e)
    {
        return new CSingleStringHandler()
        {
            StringTableKey = e?.Attribute("StringTableKey")?.Value ?? "",
            RawString = e?.Attribute("RawString")?.Value ?? "",
        };
    }

    public XElement[] ToXml()
    {
        return
        [
            new XElement("StringTableKey", StringTableKey),
            new XElement("RawString", RawString)
        ];
    }
    
    public string ToLua(string escapedname)
    {
        if (StringTableKey.Length > 0)
            return $"XGUIEng.SetTextKeyName({escapedname}, \"{StringTableKey}\")\n";
        else
            return $"XGUIEng.SetText({escapedname}, \"{RawString}\", 1)\n";
    }
}