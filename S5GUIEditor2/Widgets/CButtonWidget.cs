using System;
using System.Linq;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CButtonWidget : CBaseWidget
{
    internal new const string ClassName = "EGUIX::CButtonWidget";
    internal new const uint ClassId = 0xFE028496;
    internal CButtonHelper ButtonHelper { get; set; } = new();
    internal CMaterial MaterialsNormal { get; set; } = new();
    internal CMaterial MaterialsHover { get; set; } = new();
    internal CMaterial MaterialsPressed { get; set; } = new();
    internal CMaterial MaterialsDisabled { get; set; } = new();
    internal CMaterial MaterialsHighlighted { get; set; } = new();
    internal CToolTipHelper ToolTipHelper { get; set; } = new();
    
    internal override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e)
    {
        base.FromXml(e);
        ButtonHelper = CButtonHelper.FromXml(e?.Element("ButtonHelper"));
        var m = e?.Elements("Materials").ToArray() ?? [];
        MaterialsNormal = CMaterial.FromXml(m[0]);
        MaterialsHover = CMaterial.FromXml(m[1]);
        MaterialsPressed = CMaterial.FromXml(m[2]);
        MaterialsDisabled = CMaterial.FromXml(m[3]);
        MaterialsHighlighted = CMaterial.FromXml(m[4]);
        ToolTipHelper = CToolTipHelper.FromXml(e?.Element("ToolTipHelper"));
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("ButtonHelper", ButtonHelper.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("Materials", MaterialsNormal.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("Materials", MaterialsHover.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("Materials", MaterialsPressed.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("Materials", MaterialsDisabled.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("Materials", MaterialsHighlighted.ToXml()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("ToolTipHelper", ToolTipHelper.ToXml()));
        return e;
    }

    internal override string GetLuaCreator(string parent, string befo)
    {
        throw new InvalidOperationException("cannot create base button widget");
    }
    internal override string GetLuaData(bool ignorebef)
    {
        string escapedname = $"\"{Name}\"";
        string s = base.GetLuaData(ignorebef);
        s += $"XGUIEng.DisableButton({escapedname}, {(ButtonHelper.DisabledFlag ? "1" : "0")})\n";
        s += $"XGUIEng.HighLightButton({escapedname}, {(ButtonHelper.HighLightedFlag ? "1" : "0")})\n";
        if (ButtonHelper.ActionFunction.LuaCommand.Length > 0 && !ButtonHelper.ActionFunction.LuaCommand.StartsWith("--"))
            s += $"CppLogic.UI.ButtonOverrideActionFunc({escapedname}, function() {ButtonHelper.ActionFunction.LuaCommand} end)\n";
        if (ButtonHelper.ShortCutString.RawString.Length > 0)
            s += $"CppLogic.UI.ButtonSetShortcutString({escapedname}, \"{ButtonHelper.ShortCutString.RawString}\", false)\n";
        else if (ButtonHelper.ShortCutString.StringTableKey.Length > 0)
            s += $"CppLogic.UI.ButtonSetShortcutString({escapedname}, \"{ButtonHelper.ShortCutString.StringTableKey}\", true)\n";
        s += MaterialsNormal.ToLua(escapedname, 0);
        s += MaterialsHover.ToLua(escapedname, 1);
        s += MaterialsPressed.ToLua(escapedname, 2);
        s += MaterialsDisabled.ToLua(escapedname, 3);
        s += MaterialsHighlighted.ToLua(escapedname, 4);
        s += ToolTipHelper.ToLua(escapedname);
        return s;
    }

    internal override CToolTipHelper Tooltip => ToolTipHelper;
}