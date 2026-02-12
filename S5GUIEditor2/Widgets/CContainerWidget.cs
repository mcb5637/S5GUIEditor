using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CWidgetListHandler
{
    internal ObservableCollection<CBaseWidget> SubWidgets { get; set; } = [];

    internal static CWidgetListHandler FromXml(XElement? e)
    {
        return new CWidgetListHandler()
        {
            SubWidgets = new ObservableCollection<CBaseWidget>(e?.Elements("WidgetList").Select(CBaseWidget.GetFromXml) ?? []),
        };
    }
}

internal class CContainerWidget : CBaseWidget
{
    internal new const string ClassName = "EGUIX::CContainerWidget";
    internal new const uint ClassId = 0x55F9D1F6;
    internal CWidgetListHandler WidgetListHandler { get; set; } = new();
    
    internal override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e)
    {
        base.FromXml(e);
        WidgetListHandler = CWidgetListHandler.FromXml(e?.Element("SubWidgets"));
        foreach (var c in WidgetListHandler.SubWidgets)
            c.ParentNode = this;
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
    
    internal override string GetLuaCreator(string parent, string befo)
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
    internal override string GetLuaData(bool ignorebef)
    {
        string s = base.GetLuaData(ignorebef);
        foreach (var w in WidgetListHandler.SubWidgets)
            s += w.GetLuaData(true);
        return s;
    }
    
    internal override ObservableCollection<CBaseWidget> ChildWidgets => WidgetListHandler.SubWidgets;
}