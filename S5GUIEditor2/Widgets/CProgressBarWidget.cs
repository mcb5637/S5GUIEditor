using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CProgressBarWidget : CStaticWidget
{
    internal new const string ClassName = "EGUIX::CProgressBarWidget";
    internal new const uint ClassId = 0x72633416;
    internal UpdateFunc Update { get; set; } = new();
    internal float ProgressBarValue { get; set; } = 1.0f;
    internal float ProgressBarLimit { get; set; } = 1.0f;
    
    internal override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e)
    {
        base.FromXml(e);
        Update = UpdateFunc.FromXml(e);
        ProgressBarValue = e?.Element("ProgressBarValue")?.Value.TryParseFloat() ?? 0.0f;
        ProgressBarLimit = e?.Element("ProgressBarLimit")?.Value.TryParseFloat() ?? 0.0f;
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(Update.ToXml());
        e.Add(new XElement("ProgressBarValue", ProgressBarValue));
        e.Add(new XElement("ProgressBarLimit", ProgressBarLimit));
        return e;
    }
    
    internal override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateProgressBarWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(ignorebef);
        s += $"XGUIEng.SetProgressBarValues({escapedname}, {ProgressBarValue}, {ProgressBarLimit})\n";
        s += Update.ToLua(escapedname);
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
}