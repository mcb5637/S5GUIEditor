using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CButtonHelper
{
    internal bool DisabledFlag { get; set; }
    internal bool HighLightedFlag { get; set; }
    internal CLuaFunctionHelper ActionFunction { get; private init; } = new();
    internal CSingleStringHandler ShortCutString { get; private init; } = new();
    
    internal static CButtonHelper FromXml(XElement? e)
    {
        return new CButtonHelper()
        {
            DisabledFlag = e?.Element("DisabledFlag")?.Value.TryParseBool() ?? false,
            HighLightedFlag = e?.Element("HighLightedFlag")?.Value.TryParseBool() ?? false,
            ActionFunction = CLuaFunctionHelper.FromXml(e?.Element("ActionFunction")),
            ShortCutString = CSingleStringHandler.FromXml(e?.Element("ShortCutString")),
        };
    }

    public XElement[] ToXml()
    {
        return
        [
            new XElement("DisabledFlag", DisabledFlag.ToString().ToLower()),
            new XElement("HighLightedFlag", HighLightedFlag.ToString().ToLower()),
            new XElement("ActionFunction", ActionFunction.ToXml()),
            // ReSharper disable once CoVariantArrayConversion
            new XElement("ShortCutString", ShortCutString.ToXml()),
        ];
    }
}