using System.Globalization;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CStaticTextWidget : CStaticWidget
{
    internal new const string ClassName = "EGUIX::CStaticTextWidget";
    internal new const uint ClassId = 0x86E3BC06;
    internal CWidgetStringHelper StringHelper { get; set; } = new();
    internal UpdateFunc Update { get; set; } = new();
    internal int FirstLineToPrint { get; set; }
    internal int NumberOfLinesToPrint { get; set; }
    internal float LineDistanceFactor { get; set; }
    internal override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e)
    {
        base.FromXml(e);
        StringHelper = CWidgetStringHelper.FromXml(e?.Element("StringHelper"));
        Update = UpdateFunc.FromXml(e);
        FirstLineToPrint = e?.Element("FirstLineToPrint")?.Value.TryParseInt() ?? 0;
        NumberOfLinesToPrint = e?.Element("NumberOfLinesToPrint")?.Value.TryParseInt() ?? 0;
        LineDistanceFactor = e?.Element("LineDistanceFactor")?.Value.TryParseFloat() ?? 0.0f;
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("StringHelper", StringHelper.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(Update.ToXml());
        e.Add(new XElement("FirstLineToPrint", FirstLineToPrint.ToString()));
        e.Add(new XElement("NumberOfLinesToPrint", NumberOfLinesToPrint.ToString()));
        e.Add(new XElement("LineDistanceFactor", LineDistanceFactor.ToString(CultureInfo.InvariantCulture)));
        return e;
    }
    
    internal override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateStaticTextWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(ignorebef);
        s += StringHelper.ToLua(escapedname);
        s += Update.ToLua(escapedname);
        s += $"XGUIEng.SetLinesToPrint({escapedname}, {FirstLineToPrint}, {NumberOfLinesToPrint})\n";
        s += $"CppLogic.UI.StaticTextWidgetSetLineDistanceFactor({escapedname}, {LineDistanceFactor})\n";
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
    internal override CWidgetStringHelper TextRender => StringHelper;
}