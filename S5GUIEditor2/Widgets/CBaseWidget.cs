using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal abstract class CBaseWidget : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    internal void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    internal const string ClassName = "EGUIX::CBaseWidget";
    internal const uint ClassId = 0x18736A06;
    internal bool Visible { get; set; } = true;

    internal string Name
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Name));
        }
    } = "";

    internal RectangleF PositionAndSize { get; set; } = new();
    internal bool IsShown { get; set; }
    internal float ZPriority { get; set; }
    internal string Group { get; set; } = "";
    internal bool ForceToHandleMouseEventsFlag { get; set; }
    internal bool ForceToNeverBeFoundFlag { get; set; }
    internal CContainerWidget? ParentNode { get; set; }

    internal virtual (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal string ClassDisplayName => GetClass().Item1;
    
    internal virtual void FromXml(XElement? e, ImageCache c)
    {
        Name = e?.Element("Name")?.Value ?? "";
        IsShown = e?.Element("IsShown")?.Value.TryParseBool() ?? false;
        Visible = IsShown;
        PositionAndSize = RectangleF.FromXml(e?.Element("Rectangle"));
        ZPriority = e?.Element("ZPriority")?.Value.TryParseFloat() ?? 0.0f;
        Group = e?.Element("Group")?.Value ?? "";
        ForceToHandleMouseEventsFlag = e?.Element("ForceToHandleMouseEventsFlag")?.Value.TryParseBool() ?? false;
        ForceToNeverBeFoundFlag = e?.Element("ForceToNeverBeFoundFlag")?.Value.TryParseBool() ?? false;
    }

    internal static CBaseWidget GetFromXml(XElement? e, ImageCache c)
    {
        ArgumentNullException.ThrowIfNull(e);
        CBaseWidget r;
        var n = e.Attribute("classname")?.Value;
        if (uint.TryParse(e.Attribute("classid")?.Value.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var id))
        {
            r = id switch
            {
                CContainerWidget.ClassId => new CContainerWidget(),
                CStaticWidget.ClassId => new CStaticWidget(c),
                CGfxButtonWidget.ClassId => new CGfxButtonWidget(c),
                CTextButtonWidget.ClassId => new CTextButtonWidget(c),
                CStaticTextWidget.ClassId => new CStaticTextWidget(c),
                CCustomWidget.ClassId => new CCustomWidget(),
                CProgressBarWidget.ClassId => new CProgressBarWidget(c),
                CPureTooltipWidget.ClassId => new CPureTooltipWidget(),
                _ => throw new IOException($"invalid classid {id}")
            };

            if (n != null && r.GetClass().Item1 != n)
                throw new IOException($"classid {id} does not match classname {n}");
        }
        else
        {
            r = n switch
            {
                CContainerWidget.ClassName => new CContainerWidget(),
                CStaticWidget.ClassName => new CStaticWidget(c),
                CGfxButtonWidget.ClassName => new CGfxButtonWidget(c),
                CTextButtonWidget.ClassName => new CTextButtonWidget(c),
                CStaticTextWidget.ClassName => new CStaticTextWidget(c),
                CCustomWidget.ClassName => new CCustomWidget(),
                CProgressBarWidget.ClassName => new CProgressBarWidget(c),
                CPureTooltipWidget.ClassName => new CPureTooltipWidget(),
                _ => throw new IOException($"invalid classname {n}")
            };
        }
        r.FromXml(e, c);
        return r;
    }

    internal virtual XElement ToXml()
    {
        var e = new XElement("WidgetList", new XElement("Name", Name),
            // ReSharper disable once CoVariantArrayConversion
            new XElement("Rectangle", PositionAndSize.ToXml()),
            new XElement("IsShown", IsShown.ToString()),
            new XElement("ZPriority", ZPriority.ToString(CultureInfo.InvariantCulture)),
            new XElement("MotherID", ParentNode == null ? "" : ParentNode.Name),
            new XElement("Group", Group),
            new XElement("ForceToHandleMouseEventsFlag", ForceToHandleMouseEventsFlag.ToString()),
            new XElement("ForceToNeverBeFoundFlag", ForceToNeverBeFoundFlag.ToString()));
        var (cn, id) = GetClass();
        e.SetAttributeValue("classname", cn);
        // ReSharper disable once StringLiteralTypo
        e.SetAttributeValue("classid", id);
        return e;
    }
    
    
    private string GetNextInParent()
    {
        if (ParentNode == null)
            return "nil";
        int i = ParentNode.WidgetListHandler.SubWidgets.IndexOf(this);
        if (i >= 0 && (i + 1) < ParentNode.WidgetListHandler.SubWidgets.Count)
            return $"\"{ParentNode.WidgetListHandler.SubWidgets[i + 1].Name}\"";
        return "nil";
    }
    internal virtual string GetLuaCreator(string parent, string befo)
    {
        throw new InvalidOperationException("cannot create base widget");
    }
    internal string GetLua()
    {
        return GetLuaAssert() + GetLuaData(false);
    }
    internal virtual string GetLuaAssert()
    {
        return $"assert(XGUIEng.GetWidgetID(\"{Name}\")==0, \"{Name} already exists\")\n";
    }
    internal virtual string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        if (ParentNode == null)
            throw new InvalidOperationException("no parent widget found");
        string s = GetLuaCreator(ParentNode.Name, ignorebef ? "nil" : GetNextInParent());
        s += $"CppLogic.UI.WidgetSetPositionAndSize({escapedname}, {PositionAndSize.X}, {PositionAndSize.Y}, {PositionAndSize.Width}, {PositionAndSize.Height})\n";
        s += $"XGUIEng.ShowWidget({escapedname}, {(IsShown ? "1" : "0")})\n";
        s += $"CppLogic.UI.WidgetSetBaseData({escapedname}, {ZPriority}, {ForceToHandleMouseEventsFlag.ToString().ToLower()}, {ForceToNeverBeFoundFlag.ToString().ToLower()})\n";
        if (Group.Length > 0)
            s += $"CppLogic.UI.WidgetSetGroup({escapedname}, \"{Group}\")\n";
        return s;
    }

    internal virtual ObservableCollection<CBaseWidget> ChildWidgets => [];
    
    internal virtual CToolTipHelper? Tooltip => null;
    internal virtual CMaterial? StaticMaterial => null;
    internal virtual UpdateFunc? UpdateData => null;
    internal virtual CWidgetStringHelper? TextRender => null;
    internal virtual CMaterial? RendererMaterial => StaticMaterial;
}