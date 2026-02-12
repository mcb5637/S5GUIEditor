using System.Collections.Generic;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CCustomWidget : CBaseWidget
{
    internal new const string ClassName = "EGUIX::CCustomWidget";
    internal new const uint ClassId = 0x7656DB56;
    internal string CustomClassName { get; set; } = "";
    internal int IntegerUserVariable0DefaultValue { get; set; }
    internal int IntegerUserVariable1DefaultValue { get; set; }
    internal int IntegerUserVariable2DefaultValue { get; set; }
    internal int IntegerUserVariable3DefaultValue { get; set; }
    internal int IntegerUserVariable4DefaultValue { get; set; }
    internal int IntegerUserVariable5DefaultValue { get; set; }
    internal string StringUserVariable0DefaultValue { get; set; } = "";
    internal string StringUserVariable1DefaultValue { get; set; } = "";
    
    internal override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e)
    {
        base.FromXml(e);
        CustomClassName = e?.Element("CustomClassName")?.Value ?? "";
        IntegerUserVariable0DefaultValue = e?.Element("IntegerUserVariable0DefaultValue")?.Value.TryParseInt() ?? 0;
        IntegerUserVariable1DefaultValue = e?.Element("IntegerUserVariable1DefaultValue")?.Value.TryParseInt() ?? 0;
        IntegerUserVariable2DefaultValue = e?.Element("IntegerUserVariable2DefaultValue")?.Value.TryParseInt() ?? 0;
        IntegerUserVariable3DefaultValue = e?.Element("IntegerUserVariable3DefaultValue")?.Value.TryParseInt() ?? 0;
        IntegerUserVariable4DefaultValue = e?.Element("IntegerUserVariable4DefaultValue")?.Value.TryParseInt() ?? 0;
        IntegerUserVariable5DefaultValue = e?.Element("IntegerUserVariable5DefaultValue")?.Value.TryParseInt() ?? 0;
        StringUserVariable0DefaultValue = e?.Element("StringUserVariable0DefaultValue")?.Value ?? "";
        StringUserVariable1DefaultValue = e?.Element("StringUserVariable1DefaultValue")?.Value ?? "";
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        e.Add(new XElement("CustomClassName", this.CustomClassName));
        e.Add(new XElement("IntegerUserVariable0DefaultValue", this.IntegerUserVariable0DefaultValue.ToString()));
        e.Add(new XElement("IntegerUserVariable1DefaultValue", this.IntegerUserVariable1DefaultValue.ToString()));
        e.Add(new XElement("IntegerUserVariable2DefaultValue", this.IntegerUserVariable2DefaultValue.ToString()));
        e.Add(new XElement("IntegerUserVariable3DefaultValue", this.IntegerUserVariable3DefaultValue.ToString()));
        e.Add(new XElement("IntegerUserVariable4DefaultValue", this.IntegerUserVariable4DefaultValue.ToString()));
        e.Add(new XElement("IntegerUserVariable5DefaultValue", this.IntegerUserVariable5DefaultValue.ToString()));
        e.Add(new XElement("StringUserVariable0DefaultValue", this.StringUserVariable0DefaultValue));
        e.Add(new XElement("StringUserVariable1DefaultValue", this.StringUserVariable1DefaultValue));
        return e;
    }
    
    internal class CustomWidgetOptions
    {
        public string IntUserVar0 = "";
        public string IntUserVar1 = "";
        public string IntUserVar2 = "";
        public string IntUserVar3 = "";
        public string IntUserVar4 = "";
        public string IntUserVar5 = "";
        public string StringUserVar0 = "";
        public string StringUserVar1 = "";
        public bool SaveForExport = false;

        public string IntVar(int i)
        {
            switch (i)
            {
                case 0:
                    return IntUserVar0;
                case 1:
                    return IntUserVar1;
                case 2:
                    return IntUserVar2;
                case 3:
                    return IntUserVar3;
                case 4:
                    return IntUserVar4;
                case 5:
                    return IntUserVar5;
            }
            return "";
        }
        public string StringVar(int i)
        {
            switch (i)
            {
                case 0:
                    return StringUserVar0;
                case 1:
                    return StringUserVar1;
            }
            return "";
        }
    }

    private static Dictionary<string, CustomWidgetOptions> KnownWidgetTypes = new();
    static CCustomWidget()
    {
        KnownWidgetTypes["EGUIX::CStringInputCustomWidget"] = new CustomWidgetOptions()
        {
            IntUserVar0 = "bool alwaysVisible",
            IntUserVar1 = "bool keepContentOnClose",
            IntUserVar2 = "int mode, 0->chat, 1->password, 2->cdkey",
            IntUserVar3 = "bool noConfirmCall",
            IntUserVar4 = "int bufferSize",
            StringUserVar0 = "confirm func (inputString, widgetId)",
            SaveForExport = true,
        };
        KnownWidgetTypes["EGUIX::CScrollBarButtonCustomWidget"] = new CustomWidgetOptions()
        {
            StringUserVar0 = "confirm callback (value, widgetId)",
            StringUserVar1 = "slider gfx source name",
            SaveForExport = false,
        };
        KnownWidgetTypes["EGUIX::CVideoPlaybackCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::C3DOnScreenInformationCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::CShortMessagesWindowControllerCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::CStatisticsRendererCustomWidget"] = new CustomWidgetOptions()
        {
            IntUserVar0 = "bool isMainmenu",
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::CMiniMapCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::CMiniMapOverlayCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::CNetworkInfoCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::CNetworkWindowControllerCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::C3DOnScreenDebugCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["GGUI::CNotesWindowControllerCustomWidget"] = new CustomWidgetOptions()
        {
            SaveForExport = false,
        };
        KnownWidgetTypes["CppLogic::Mod::UI::AutoScrollCustomWidget"] = new CustomWidgetOptions()
        {
            IntUserVar0 = "int spacing",
            StringUserVar0 = "slider widget",
            StringUserVar1 = "scrollable widget (optional)",
            SaveForExport = true,
        };
        KnownWidgetTypes["CppLogic::Mod::UI::TextInputCustomWidget"] = new CustomWidgetOptions()
        {
            IntUserVar0 = "int mode 0->normal, 1->password, 2->int, 3->double, 4->uint, 5->udouble",
            IntUserVar1 = "flags 1->fire cancel event, 2->fire validate event",
            IntUserVar2 = "argb text color (white, if a==0)",
            IntUserVar3 = "argb cursor highlight color (gray, if a==0)",
            IntUserVar4 = "argb background color (none, if a==0)",
            IntUserVar5 = "int scroll delta",
            StringUserVar0 = "event func (text, widgetid, event) event: 0->confirm, 1->cancel, 2->validate",
            StringUserVar1 = "font (optional)",
            SaveForExport = true,
        };
        KnownWidgetTypes["CppLogic::Mod::UI::FreeCamCustomWidget"] = new CustomWidgetOptions()
        {
            IntUserVar0 = "default scroll speed",
            SaveForExport = true,
        };
    }

    internal static CustomWidgetOptions? TryGet(string className)
    {
        return KnownWidgetTypes.GetValueOrDefault(className);
    }

    internal override string GetLuaCreator(string parent, string befo)
    {
        //var o = TryGet(CustomClassName);
        //if (o == null || !o.SaveForExport)
        //    MessageBox.Show($"Warning: CustomWidget {Name} of type {CustomClassName} in export, make sure this works properly.\nLook at the CppLogic.UI.ContainerWidgetCreateCustomWidgetChild documentation for more info.", "CustomWidget export");
        return $"CppLogic.UI.ContainerWidgetCreateCustomWidgetChild(\"{parent}\", \"{Name}\", \"{CustomClassName}\", {befo}, {IntegerUserVariable0DefaultValue}, {IntegerUserVariable1DefaultValue}, {IntegerUserVariable2DefaultValue}, {IntegerUserVariable3DefaultValue}, {IntegerUserVariable4DefaultValue}, {IntegerUserVariable5DefaultValue}, \"{StringUserVariable0DefaultValue.Replace("\\", @"\\")}\", \"{StringUserVariable1DefaultValue.Replace("\\", @"\\")}\")\n";
    }
}