using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using Avalonia.Media;

namespace S5GUIEditor2.Widgets;

internal class IntUserVarConfig : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal required CCustomWidget.IntUserVar Value
    {
        get;
        init
        {
            field = value;
            field.PropertyChanged += (_, _) =>
            {
                OnPropertyChanged(nameof(SelectedMode));
                OnPropertyChanged(nameof(Flag1));
                OnPropertyChanged(nameof(Flag2));
            };
        }
    }
    internal required CCustomWidget.CustomWidgetOptionsIntVar Config { get; init; }

    internal CCustomWidget.NamedInt? SelectedMode
    {
        get => Config.Modes.FirstOrDefault(x => x.Value == Value.Value);
        set => Value.Value = value?.Value ?? 0;
    }

    internal bool Flag1
    {
        get => Value.GetFlag(Config.Flags.FirstOrDefault()?.Value ?? 1);
        set => Value.SetFlag(Config.Flags.FirstOrDefault()?.Value ?? 1, value);
    }
    internal bool Flag2
    {
        get => Value.GetFlag(Config.Flags.Skip(1).FirstOrDefault()?.Value ?? 2);
        set => Value.SetFlag(Config.Flags.Skip(1).FirstOrDefault()?.Value ?? 2, value);
    }

    internal string Flag1Name => Config.Flags.FirstOrDefault()?.Name ?? "";
    internal string Flag2Name => Config.Flags.Skip(1).FirstOrDefault()?.Name ?? "";
}

internal class CCustomWidget : CBaseWidget
{
    internal const string ClassName = "EGUIX::CCustomWidget";
    internal const uint ClassId = 0x7656DB56;

    private string CustomClassName
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(CustomClassName));
            OnPropertyChanged(nameof(IntUserVar0));
            OnPropertyChanged(nameof(IntUserVar1));
            OnPropertyChanged(nameof(IntUserVar2));
            OnPropertyChanged(nameof(IntUserVar3));
            OnPropertyChanged(nameof(IntUserVar4));
            OnPropertyChanged(nameof(IntUserVar5));
            OnPropertyChanged(nameof(SelectedTypeStringDesc0));
            OnPropertyChanged(nameof(SelectedTypeStringDesc1));
        }
    } = "EGUIX::CVideoPlaybackCustomWidget";
    
    internal class IntUserVar : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal int Value
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(Col));
            }
        }

        internal Color Col
        {
            get => Color.FromArgb((byte)(Value >> 24),  (byte)(Value >> 16 & 0xFF), (byte)(Value >> 8 & 0xFF), (byte)(Value & 0xFF));
            set => Value = value.A << 24 | value.B << 16 | value.G << 8 | value.R;
        }

        internal IntUserVar(int v)
        {
            Value = v;
        }

        internal bool GetFlag(int n)
        {
            return (Value & (1 << n)) != 0;
        }

        internal void SetFlag(int n, bool v)
        {
            if (v)
                Value |= 1 << n;
            else
                Value &= ~(1 << n);
        }
    }
    
    private IntUserVar IntegerUserVariable0DefaultValue
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(IntUserVar0));
        }
    } = new(0);

    private IntUserVar IntegerUserVariable1DefaultValue
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(IntUserVar1));
        }
    } = new(0);
    private IntUserVar IntegerUserVariable2DefaultValue
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(IntUserVar2));
        }
    } = new(0);
    private IntUserVar IntegerUserVariable3DefaultValue
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(IntUserVar3));
        }
    } = new(0);
    private IntUserVar IntegerUserVariable4DefaultValue
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(IntUserVar4));
        }
    } = new(0);
    private IntUserVar IntegerUserVariable5DefaultValue
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(IntUserVar5));
        }
    } = new(0);
    internal string StringUserVariable0DefaultValue { get; set; } = "";
    internal string StringUserVariable1DefaultValue { get; set; } = "";

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        CustomClassName = e?.Element("CustomClassName")?.Value ?? "";
        IntegerUserVariable0DefaultValue = new IntUserVar(e?.Element("IntegerUserVariable0DefaultValue")?.Value.TryParseInt() ?? 0);
        IntegerUserVariable1DefaultValue = new IntUserVar(e?.Element("IntegerUserVariable1DefaultValue")?.Value.TryParseInt() ?? 0);
        IntegerUserVariable2DefaultValue = new IntUserVar(e?.Element("IntegerUserVariable2DefaultValue")?.Value.TryParseInt() ?? 0);
        IntegerUserVariable3DefaultValue = new IntUserVar(e?.Element("IntegerUserVariable3DefaultValue")?.Value.TryParseInt() ?? 0);
        IntegerUserVariable4DefaultValue = new IntUserVar(e?.Element("IntegerUserVariable4DefaultValue")?.Value.TryParseInt() ?? 0);
        IntegerUserVariable5DefaultValue = new IntUserVar(e?.Element("IntegerUserVariable5DefaultValue")?.Value.TryParseInt() ?? 0);
        StringUserVariable0DefaultValue = e?.Element("StringUserVariable0DefaultValue")?.Value ?? "";
        StringUserVariable1DefaultValue = e?.Element("StringUserVariable1DefaultValue")?.Value ?? "";
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        e.Add(new XElement("CustomClassName", CustomClassName));
        e.Add(new XElement("IntegerUserVariable0DefaultValue", IntegerUserVariable0DefaultValue.Value.ToString()));
        e.Add(new XElement("IntegerUserVariable1DefaultValue", IntegerUserVariable1DefaultValue.Value.ToString()));
        e.Add(new XElement("IntegerUserVariable2DefaultValue", IntegerUserVariable2DefaultValue.Value.ToString()));
        e.Add(new XElement("IntegerUserVariable3DefaultValue", IntegerUserVariable3DefaultValue.Value.ToString()));
        e.Add(new XElement("IntegerUserVariable4DefaultValue", IntegerUserVariable4DefaultValue.Value.ToString()));
        e.Add(new XElement("IntegerUserVariable5DefaultValue", IntegerUserVariable5DefaultValue.Value.ToString()));
        e.Add(new XElement("StringUserVariable0DefaultValue", StringUserVariable0DefaultValue));
        e.Add(new XElement("StringUserVariable1DefaultValue", StringUserVariable1DefaultValue));
        return e;
    }

    internal class NamedInt
    {
        internal string Name { get; init; }
        internal int Value { get; init; }

        internal NamedInt(string n, int v)
        {
            Name = n;
            Value = v;
        }
    }
    internal class CustomWidgetOptionsIntVar
    {
        internal string Description { get; init; } = "";
        internal bool IsColor { get; init; } = false;
        internal ObservableCollection<NamedInt> Modes { get; set; } = [];
        internal NamedInt[] Flags { get; init; } = [];
        internal bool IsModes => Modes.Count > 0;
        internal bool IsFlags => Flags.Length > 0;
        internal bool HasFlag2 => Flags.Length > 1;
    }
    
    internal class CustomWidgetOptions
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        internal required string ClassName { get; init; }
        internal CustomWidgetOptionsIntVar IntUserVar0 { get; init; } = new();
        internal CustomWidgetOptionsIntVar IntUserVar1 { get; init; } = new();
        internal CustomWidgetOptionsIntVar IntUserVar2 { get; init; } = new();
        internal CustomWidgetOptionsIntVar IntUserVar3 { get; init; } = new();
        internal CustomWidgetOptionsIntVar IntUserVar4 { get; init; } = new();
        internal CustomWidgetOptionsIntVar IntUserVar5 { get; init; } = new();
        internal string StringUserVar0 { get; init; } = "";
        internal string StringUserVar1 { get; init; } = "";
        internal bool SaveForExport = false;
    }

    private static readonly List<CustomWidgetOptions> KnownWidgetTypes =
    [
        new()
        {
            ClassName = "EGUIX::CStringInputCustomWidget",
            IntUserVar0 = new CustomWidgetOptionsIntVar(){
                Description = "bool alwaysVisible",
                Flags = [new NamedInt("alwaysVisible", 0)]
            },
            IntUserVar1 = new CustomWidgetOptionsIntVar(){
                Description = "bool keepContentOnClose",
                Flags = [new NamedInt("keepContentOnClose", 0)]
            },
            IntUserVar2 = new CustomWidgetOptionsIntVar(){
                Description = "int mode, 0->chat, 1->password, 2->cdkey",
                Modes = [new NamedInt("chat", 0), new NamedInt("password", 1), new NamedInt("cdkey", 2)]
            },
            IntUserVar3 = new CustomWidgetOptionsIntVar(){
                Description = "bool noConfirmCall",
                Flags = [new NamedInt("noConfirmCall", 0)]
            },
            IntUserVar4 = new CustomWidgetOptionsIntVar(){
                Description = "int bufferSize",
            },
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
            IntUserVar0 = new CustomWidgetOptionsIntVar(){
                Description = "bool isMainmenu",
                Flags = [new NamedInt("isMainmenu", 0)]
            },
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
            IntUserVar0 = new CustomWidgetOptionsIntVar(){
                Description = "int spacing",
            },
            StringUserVar0 = "slider widget (optional)",
            StringUserVar1 = "scrollable widget",
            SaveForExport = true,
        },
        new()
        {
            ClassName = "CppLogic::Mod::UI::TextInputCustomWidget",
            IntUserVar0 = new CustomWidgetOptionsIntVar(){
                Description = "int mode 0->normal, 1->password, 2->int, 3->double, 4->uint, 5->udouble",
                Modes = [new NamedInt("normal", 0),  new NamedInt("password", 1), new NamedInt("int", 2),
                    new NamedInt("double", 3), new NamedInt("uint", 4), new NamedInt("udouble", 5)]
            },
            IntUserVar1 = new CustomWidgetOptionsIntVar(){
                Description = "flags 1->fire cancel event, 2->fire validate event",
                Flags = [new NamedInt("fire cancel event", 0), new NamedInt("fire validate event", 1)]
            },
            IntUserVar2 = new CustomWidgetOptionsIntVar(){
                Description = "argb text color (white, if a==0)",
                IsColor = true,
            },
            IntUserVar3 = new CustomWidgetOptionsIntVar(){
                Description = "argb cursor highlight color (gray, if a==0)",
                IsColor = true,
            },
            IntUserVar4 = new CustomWidgetOptionsIntVar(){
                Description = "argb background color (none, if a==0)",
                IsColor = true,
            },
            IntUserVar5 = new CustomWidgetOptionsIntVar(){
                Description = "int scroll delta",
            },
            StringUserVar0 = "event func (text, widgetid, event) event: 0->confirm, 1->cancel, 2->validate",
            StringUserVar1 = "font (optional)",
            SaveForExport = true,
        },
        new()
        {
            ClassName = "CppLogic::Mod::UI::FreeCamCustomWidget",
            IntUserVar0 = new CustomWidgetOptionsIntVar(){
                Description = "default scroll speed",
            },
            SaveForExport = true,
        },
    ];

    protected override string GetLuaCreator(string parent, string befo)
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

    private static readonly CustomWidgetOptionsIntVar IntEmptyConfig = new();
    internal IntUserVarConfig IntUserVar0 => new()
    {
        Config = SelectedType?.IntUserVar0 ?? IntEmptyConfig,
        Value = IntegerUserVariable0DefaultValue,
    };
    internal IntUserVarConfig IntUserVar1 => new()
    {
        Config = SelectedType?.IntUserVar1 ?? IntEmptyConfig,
        Value = IntegerUserVariable1DefaultValue,
    };
    internal IntUserVarConfig IntUserVar2 => new()
    {
        Config = SelectedType?.IntUserVar2 ?? IntEmptyConfig,
        Value = IntegerUserVariable2DefaultValue,
    };
    internal IntUserVarConfig IntUserVar3 => new()
    {
        Config = SelectedType?.IntUserVar3 ?? IntEmptyConfig,
        Value = IntegerUserVariable3DefaultValue,
    };
    internal IntUserVarConfig IntUserVar4 => new()
    {
        Config = SelectedType?.IntUserVar4 ?? IntEmptyConfig,
        Value = IntegerUserVariable4DefaultValue,
    };
    internal IntUserVarConfig IntUserVar5 => new()
    {
        Config = SelectedType?.IntUserVar5 ?? IntEmptyConfig,
        Value = IntegerUserVariable5DefaultValue,
    };
}