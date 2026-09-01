using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CTextButtonWidget : CButtonWidget
{
    internal const string ClassName = "EGUIX::CTextButtonWidget";
    internal const uint ClassId = 0x5DD085A6;

    internal CTextButtonWidget(ImageCache c) : base(c)
    {
        StringHelper = new()
        {
            Font = new()
            {
                Cache = c,
            }
        };
    }

    private CWidgetStringHelper StringHelper
    {
        get;
        set
        {
            field?.PropertyChanged -= PropChanged;
            field = value;
            field.PropertyChanged += PropChanged;
            return;
            
            void PropChanged(object? o, PropertyChangedEventArgs propertyChangedEventArgs)
            {
                OnPropertyChanged(nameof(StringHelper));
                ReValidate();
            }
        }
    }
    internal bool CppLogicCenterText { get; set; } = true;
    private UpdateFunc Update { get; set; } = new();

    protected override (string, uint) GetClass()
    {
        return (ClassName, ClassId);
    }

    internal override void FromXml(XElement? e, ImageCache c)
    {
        base.FromXml(e, c);
        StringHelper = CWidgetStringHelper.FromXml(e?.Element("StringHelper"), c);
        CppLogicCenterText = e?.Element("CenterText")?.Value.TryParseBool() ?? true;
        Update = UpdateFunc.FromXml(e);
    }

    internal override XElement ToXml()
    {
        var e = base.ToXml();
        // ReSharper disable once CoVariantArrayConversion
        e.Add(new XElement("StringHelper", StringHelper.ToXml()));
        e.Add(new XElement("CenterText", CppLogicCenterText.ToString().ToLower()));
        // ReSharper disable once CoVariantArrayConversion
        e.Add(Update.ToXml());
        return e;
    }

    protected override string GetLuaCreator(string parent, string befo)
    {
        return $"CppLogic.UI.ContainerWidgetCreateTextButtonWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
    }
    internal override string GetLuaData(IList<CBaseWidget> existing, string escapedName,
        CBaseWidget? prev)
    {
        string s = base.GetLuaData(existing, escapedName, prev);
        s += StringHelper.ToLua(escapedName);
        s += $"CppLogic.UI.TextButtonSetCenterText(\"{Name}\", {CppLogicCenterText.ToString().ToLower()})\n";
        s += Update.ToLua(escapedName);
        return s;
    }

    internal override UpdateFunc UpdateData => Update;
    internal override CWidgetStringHelper TextRender => StringHelper;
    
    internal override IEnumerable<string> ReferencedFiles => [
        StringHelper.Font.FontName,
        MaterialsNormal.Texture,
        MaterialsHover.Texture,
        MaterialsPressed.Texture,
        MaterialsDisabled.Texture,
        MaterialsHighlighted.Texture,
    ];

    internal override (string, CBaseWidget)? Validate
    {
        get
        {
            var b = base.Validate;
            if (b != null)
                return b;
            if (StringHelper.Validate)
                return ($"{Name} invalid font extension", this);
            return null;
        }
    }
}