using System;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CProjectWidget : CContainerWidget
{
    internal new const string ClassName = "EGUIX::CProjectWidget";
    internal new const uint ClassId = 0x5CA15E96;
    
    internal override (string, uint) GetClass()
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
    internal override string GetLuaCreator(string parent, string befo)
    {
        throw new InvalidOperationException("cannot create root widget");
    }
}