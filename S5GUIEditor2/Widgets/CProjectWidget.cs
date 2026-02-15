using System;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CProjectWidget : CContainerWidget
{
    private new const string ClassName = "EGUIX::CProjectWidget";
    private new const uint ClassId = 0x5CA15E96;

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override XElement ToXml()
    {
        var xe = base.ToXml();
        xe.Add(new XElement("CurrentRootWidget", WidgetListHandler.SubWidgets.Count < 1 ? "" : WidgetListHandler.SubWidgets[0].Name));
        xe.RemoveAttributes();
        xe.Name = "root";
        return xe;
    }

    protected override string GetLuaCreator(string parent, string befo)
    {
        throw new InvalidOperationException("cannot create root widget");
    }
}