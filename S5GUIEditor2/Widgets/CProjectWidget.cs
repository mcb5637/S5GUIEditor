using System;
using System.Collections.Generic;
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

    internal override (string, CBaseWidget)? Validate
    {
        get
        {
            var r = base.Validate;
            if (r != null)
                return r;

            Dictionary<string, CBaseWidget> nameLookup = [];
            
            return Check(this);

            (string, CBaseWidget)? Check(CBaseWidget w)
            {
                if (!nameLookup.TryAdd(w.Name, w))
                    return ($"{w.Name} exists multiple times", w);
                if (w is CContainerWidget cw)
                {
                    foreach (var c in cw.WidgetListHandler.SubWidgets)
                    {
                        var r2 = Check(c);
                        if (r2 != null)
                            return r2;
                    }
                }
                return null;
            }
        }
    }
}