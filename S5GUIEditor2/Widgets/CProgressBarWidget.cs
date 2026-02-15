using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CProgressBarWidget : CStaticWidget
{
    internal new const string ClassName = "EGUIX::CProgressBarWidget";
    internal new const uint ClassId = 0x72633416;

    internal CProgressBarWidget(ImageCache c) : base(c)
    {
    }

    private UpdateFunc Update { get; set; } = new();
    internal float ProgressBarValue { get; set; } = 50.0f;
    internal float ProgressBarLimit { get; set; } = 100.0f;

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
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

    protected override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateProgressBarWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(string before)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(before);
        s += $"XGUIEng.SetProgressBarValues({escapedname}, {ProgressBarValue}, {ProgressBarLimit})\n";
        s += Update.ToLua(escapedname);
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
}