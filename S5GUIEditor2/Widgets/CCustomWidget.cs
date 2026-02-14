using System.Collections.Generic;
using System.Linq;
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

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
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
        // ReSharper disable once MemberHidesStaticFromOuterClass
        internal required string ClassName { get; init; }
        internal string IntUserVar0  { get; init; }= "";
        internal string IntUserVar1  { get; init; }= "";
        internal string IntUserVar2  { get; init; }= "";
        internal string IntUserVar3  { get; init; }= "";
        internal string IntUserVar4  { get; init; }= "";
        internal string IntUserVar5  { get; init; }= "";
        internal string StringUserVar0  { get; init; }= "";
        internal string StringUserVar1  { get; init; }= "";
        internal bool SaveForExport = false;

        internal string IntVar(int i)
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
        internal string StringVar(int i)
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

    private static List<CustomWidgetOptions> KnownWidgetTypes =
    [
        new()
        {
            ClassName = "EGUIX::CStringInputCustomWidget",
            IntUserVar0 = "bool alwaysVisible",
            IntUserVar1 = "bool keepContentOnClose",
            IntUserVar2 = "int mode, 0->chat, 1->password, 2->cdkey",
            IntUserVar3 = "bool noConfirmCall",
            IntUserVar4 = "int bufferSize",
            StringUserVar0 = "confirm func (inputString, widgetId)",
            SaveForExport = true,
        },
        new()
        {
            ClassName = "EGUIX::CScrollBarButtonCustomWidget",
            StringUserVar0 = "confirm callback (value, widgetId)",
            StringUserVar1 = "slider gfx source name",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "EGUIX::CVideoPlaybackCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::C3DOnScreenInformationCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::CShortMessagesWindowControllerCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::CStatisticsRendererCustomWidget",
            IntUserVar0 = "bool isMainmenu",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::CMiniMapCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::CMiniMapOverlayCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::CNetworkInfoCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::CNetworkWindowControllerCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::C3DOnScreenDebugCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::CNotesWindowControllerCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "GGUI::C3DViewCustomWidget",
            SaveForExport = false,
        },
        new()
        {
            ClassName = "CppLogic::Mod::UI::AutoScrollCustomWidget",
            IntUserVar0 = "int spacing",
            StringUserVar0 = "slider widget (optional)",
            StringUserVar1 = "scrollable widget",
            SaveForExport = true,
        },
        new()
        {
            ClassName = "CppLogic::Mod::UI::TextInputCustomWidget",
            IntUserVar0 = "int mode 0->normal, 1->password, 2->int, 3->double, 4->uint, 5->udouble",
            IntUserVar1 = "flags 1->fire cancel event, 2->fire validate event",
            IntUserVar2 = "argb text color (white, if a==0)",
            IntUserVar3 = "argb cursor highlight color (gray, if a==0)",
            IntUserVar4 = "argb background color (none, if a==0)",
            IntUserVar5 = "int scroll delta",
            StringUserVar0 = "event func (text, widgetid, event) event: 0->confirm, 1->cancel, 2->validate",
            StringUserVar1 = "font (optional)",
            SaveForExport = true,
        },
        new()
        {
            ClassName = "CppLogic::Mod::UI::FreeCamCustomWidget",
            IntUserVar0 = "default scroll speed",
            SaveForExport = true,
        },
    ];

    internal override string GetLuaCreator(string parent, string befo)
    {
        //var o = TryGet(CustomClassName);
        //if (o == null || !o.SaveForExport)
        //    MessageBox.Show($"Warning: CustomWidget {Name} of type {CustomClassName} in export, make sure this works properly.\nLook at the CppLogic.UI.ContainerWidgetCreateCustomWidgetChild documentation for more info.", "CustomWidget export");
        return $"CppLogic.UI.ContainerWidgetCreateCustomWidgetChild(\"{parent}\", \"{Name}\", \"{CustomClassName}\", {befo}, {IntegerUserVariable0DefaultValue}, {IntegerUserVariable1DefaultValue}, {IntegerUserVariable2DefaultValue}, {IntegerUserVariable3DefaultValue}, {IntegerUserVariable4DefaultValue}, {IntegerUserVariable5DefaultValue}, \"{StringUserVariable0DefaultValue.Replace("\\", @"\\")}\", \"{StringUserVariable1DefaultValue.Replace("\\", @"\\")}\")\n";
    }

    internal static List<CustomWidgetOptions> SelectableTypes => KnownWidgetTypes;

    internal CustomWidgetOptions? SelectedType
    {
        get => KnownWidgetTypes.FirstOrDefault(x => x.ClassName == CustomClassName);
        set => CustomClassName = value?.ClassName ?? KnownWidgetTypes.First().ClassName;
    }

    internal string SelectedTypeStringDesc0 => SelectedType?.StringUserVar0 ?? "";
    internal string SelectedTypeStringDesc1 => SelectedType?.StringUserVar1 ?? "";
}