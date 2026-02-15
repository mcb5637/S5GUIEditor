using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CWidgetListHandler
{
    internal ObservableCollection<CBaseWidget> SubWidgets { get; private init; } = [];

    internal static CWidgetListHandler FromXml(XElement? e, ImageCache c)
    {
        return new CWidgetListHandler()
        {
            SubWidgets = new ObservableCollection<CBaseWidget>(e?.Elements("WidgetList").Select(x => CBaseWidget.GetFromXml(x, c)) ?? []),
        };
    }
}

internal class CContainerWidget : CBaseWidget
{
    internal const string ClassName = "EGUIX::CContainerWidget";
    internal const uint ClassId = 0x55F9D1F6;
    internal CWidgetListHandler WidgetListHandler { get; private set; } = new();

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        WidgetListHandler = CWidgetListHandler.FromXml(e?.Element("SubWidgets"), c);
        foreach (var widget in WidgetListHandler.SubWidgets)
            widget.ParentNode = this;
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        var s = new XElement("SubWidgets");
        foreach (var c in WidgetListHandler.SubWidgets)
            s.Add(c.ToXml());
        e.Add(s);
        return e;
    }

    protected override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateContainerWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaAssert()
    {
        string r = base.GetLuaAssert();
        foreach (var w in WidgetListHandler.SubWidgets)
            r += w.GetLuaAssert();
        return r;
    }
    internal override string GetLuaData(string before)
    {
        string s = base.GetLuaData(before);
        foreach (var w in WidgetListHandler.SubWidgets)
            s += w.GetLuaData("nil");
        return s;
    }
    
    internal override ObservableCollection<CBaseWidget> ChildWidgets => WidgetListHandler.SubWidgets;
}