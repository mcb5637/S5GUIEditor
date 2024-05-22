using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DevIL;

namespace S5GUIEditor
{
    public abstract class Widget
    {
        public static Dictionary<Type, WidgetType> WidgetTypes = new Dictionary<Type, WidgetType>()
        {
            {typeof(ContainerWidget), new WidgetType("0x55f9d1f6", "EGUIX::CContainerWidget", "Widget")},
            {typeof(StaticWidget), new WidgetType("0x213a8776", "EGUIX::CStaticWidget", "Background")},
            {typeof(GfxButtonWidget), new WidgetType("0x56dda656", "EGUIX::CGfxButtonWidget", "GFX")},
            {typeof(TextButtonWidget), new WidgetType("0x5dd085a6", "EGUIX::CTextButtonWidget", "Text")},
            {typeof(StaticTextWidget), new WidgetType("0x86e3bc06", "EGUIX::CStaticTextWidget", "Text")},
            {typeof(CustomWidget), new WidgetType("0x7656db56", "EGUIX::CCustomWidget", "Custom Widget")},
            {typeof(ProgressBarWidget), new WidgetType("0x72633416", "EGUIX::CProgressBarWidget", "Progress Bar")},
            {typeof(PureTooltipWidget), new WidgetType("0x82cc8876", "EGUIX::CPureTooltipWidget", "Tooltip")},
            {typeof(Widget), new WidgetType("Oh N0!", "Oh No!", "Widget")},
            {typeof(ButtonWidget), new WidgetType("Oh N0!", "Oh No!", "Button")},
            {typeof(SpecialBonusBaseShitWidget), new WidgetType("0x55f9d1f6", "EGUIX::CContainerWidget", "OH NO!")}
        };
        private static Dictionary<string, Type> stringToType;
        public static Type TypeFromString(string typeName)
        {
            if (stringToType == null)
            {
                stringToType = new Dictionary<string, Type>();
                foreach (KeyValuePair<Type, WidgetType> kvp in WidgetTypes)
                    if (!stringToType.ContainsKey(kvp.Value.ClassName))
                        stringToType.Add(kvp.Value.ClassName, kvp.Key);
            }

            return stringToType[typeName];
        }
        public bool Visible { get; set; }
        public TreeNode TreeNode { get; set; }
        public ContainerWidget ParentNode { get; set; }
        public string Name { get; set; }
        public RectangleF PositionAndSize { get; set; }
        public RectangleF CurrentPositionAndSize { get; set; }
        public bool IsShown { get; set; }
        public float ZPriority { get; set; }
        public string Group { get; set; } // use and data type?
        public bool ForceToHandleMouseEventsFlag { get; set; }
        public bool ForceToNeverBeFoundFlag { get; set; }
        protected virtual List<string> GetDialogPages()
        {
            return new List<string>() { "Widget" };
        }
        public List<string> OpenSettingDialog()
        {
            return this.GetDialogPages();
        }

        public Widget(XElement e, ContainerWidget parentNode)
        {
            this.Name = e.Element("Name").Value;
            this.TreeNode = new TreeNode(this.Name);

            if (this is ContainerWidget)
            {
                this.TreeNode.ImageIndex = 0;
                this.TreeNode.SelectedImageIndex = 0;
            }
            else
            {
                this.TreeNode.ImageIndex = 1;
                this.TreeNode.SelectedImageIndex = 1;
            }
            this.ParentNode = parentNode;
            this.TreeNode.Tag = this;
            if (parentNode != null)
                parentNode.TreeNode.Nodes.Add(this.TreeNode);
            XElement rectangle = e.Element("Rectangle");
            this.PositionAndSize = XmlConverter.ToRectangleF(rectangle);
            this.IsShown = bool.Parse(e.Element("IsShown").Value);
            this.Visible = this.IsShown;
            this.TreeNode.Checked = this.IsShown;
            this.ZPriority = float.Parse(e.Element("ZPriority").Value);
            this.Group = e.Element("Group").Value;
            this.ForceToHandleMouseEventsFlag = bool.Parse(e.Element("ForceToHandleMouseEventsFlag").Value);
            this.ForceToNeverBeFoundFlag = bool.Parse(e.Element("ForceToNeverBeFoundFlag").Value);
        }

        public Widget(ContainerWidget parentNode, RectangleF positionAndSize)
        {
            this.Visible = true;
            this.Name = "New" + Widget.WidgetTypes[this.GetType()].ClassName.Substring(8) + GlobalSettings.GetUniqueInt();
            this.TreeNode = new TreeNode(this.Name);
            if (this is ContainerWidget)
            {
                this.TreeNode.ImageIndex = 0;
                this.TreeNode.SelectedImageIndex = 0;
            }
            else
            {
                this.TreeNode.ImageIndex = 1;
                this.TreeNode.SelectedImageIndex = 1;
            }
            this.ParentNode = parentNode;
            this.TreeNode.Tag = this;
            if (parentNode != null)
                parentNode.TreeNode.Nodes.Add(this.TreeNode);
            this.PositionAndSize = positionAndSize;
            this.IsShown = false;
            this.Visible = true;
            this.TreeNode.Checked = true;
            this.ZPriority = 0f;
            this.Group = "";
            this.ForceToHandleMouseEventsFlag = false;
            this.ForceToNeverBeFoundFlag = false;
        }

        public static Widget ParseWidget(XElement e, ContainerWidget parentNode)
        {
            Widget widget;
            switch (e.FirstAttribute.Value)
            {
                case "EGUIX::CContainerWidget":
                    widget = new ContainerWidget(e, parentNode);
                    break;
                case "EGUIX::CStaticWidget":
                    widget = new StaticWidget(e, parentNode);
                    break;
                case "EGUIX::CGfxButtonWidget":
                    widget = new GfxButtonWidget(e, parentNode);
                    break;
                case "EGUIX::CTextButtonWidget":
                    widget = new TextButtonWidget(e, parentNode);
                    break;
                case "EGUIX::CStaticTextWidget":
                    widget = new StaticTextWidget(e, parentNode);
                    break;
                case "EGUIX::CCustomWidget":
                    widget = new CustomWidget(e, parentNode);
                    break;
                case "EGUIX::CProgressBarWidget":
                    widget = new ProgressBarWidget(e, parentNode);
                    break;
                case "EGUIX::CPureTooltipWidget":
                    widget = new PureTooltipWidget(e, parentNode);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return widget;
        }

        public virtual XElement GetXml()
        {
            XElement[] elements = new XElement[] {
                new XElement("Name", this.Name),
                new XElement("Rectangle", XmlConverter.FromRectangleF(this.PositionAndSize)),
                new XElement("IsShown", this.IsShown.ToString()),
                new XElement("ZPriority", this.ZPriority.ToString()),
                new XElement("MotherID", this.ParentNode == null ? "" : this.ParentNode.Name),
                new XElement("Group", this.Group),
                new XElement("ForceToHandleMouseEventsFlag", this.ForceToHandleMouseEventsFlag.ToString()),
                new XElement("ForceToNeverBeFoundFlag", this.ForceToNeverBeFoundFlag.ToString())
            };
            XElement widgetElement;
            if (this is SpecialBonusBaseShitWidget)
                widgetElement = new XElement("root", elements);
            else
            {
                widgetElement = new XElement("WidgetList", elements);
                WidgetType widgetType = Widget.WidgetTypes[this.GetType()];
                widgetElement.SetAttributeValue("classname", widgetType.ClassName);
                widgetElement.SetAttributeValue("classid", widgetType.ClassId);
            }
            return widgetElement;
        }
        private string GetNextInParent()
        {
            if (ParentNode == null)
                return "nil";
            int i = ParentNode.SubWidgets.IndexOf(this);
            if (i >= 0 && (i + 1) < ParentNode.SubWidgets.Count)
                return $"\"{ParentNode.SubWidgets[i + 1].Name}\"";
            return "nil";
        }
        protected virtual string GetLuaCreator(string parent, string befo)
        {
            throw new InvalidOperationException("cannot create base widget");
        }
        public string GetLua()
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
            string s = GetLuaCreator(ParentNode.Name, ignorebef ? "nil" : GetNextInParent());
            if (ParentNode == null)
                throw new InvalidOperationException("no parent widget found");
            s += $"CppLogic.UI.WidgetSetPositionAndSize({escapedname}, {PositionAndSize.X}, {PositionAndSize.Y}, {PositionAndSize.Width}, {PositionAndSize.Height})\n";
            s += $"XGUIEng.ShowWidget({escapedname}, {(IsShown ? "1" : "0")})\n";
            s += $"CppLogic.UI.WidgetSetBaseData({escapedname}, {ZPriority}, {ForceToHandleMouseEventsFlag.ToString().ToLower()}, {ForceToNeverBeFoundFlag.ToString().ToLower()})\n";
            if (Group.Length > 0)
                s += $"CppLogic.UI.WidgetSetGroup({escapedname}, \"{Group}\")\n";
            return s;
        }
        public virtual void DrawWidget(Graphics g, float zoom, PointF origin)
        {

            float x = this.PositionAndSize.X * zoom + origin.X;
            float y = this.PositionAndSize.Y * zoom + origin.Y;
            float width = this.PositionAndSize.Width * zoom;
            float height = this.PositionAndSize.Height * zoom;
            this.CurrentPositionAndSize = new RectangleF(x, y, width, height);
        }

        public virtual void ShowWidget(Graphics g)
        {
            g.FillRectangle(GlobalSettings.BgBrush, this.CurrentPositionAndSize);
            g.DrawRectangle(Pens.Black, this.CurrentPositionAndSize.X, this.CurrentPositionAndSize.Y, this.CurrentPositionAndSize.Width, this.CurrentPositionAndSize.Height);
        }

        public bool IsInWidget(Point position)
        {
            if (position.X > this.CurrentPositionAndSize.X && position.X < (this.CurrentPositionAndSize.X + this.CurrentPositionAndSize.Width))
                if (position.Y > this.CurrentPositionAndSize.Y && position.Y < (this.CurrentPositionAndSize.Y + this.CurrentPositionAndSize.Height))
                    return true;
            return false;
        }

        public virtual Widget GetWidgetUnderPosition(Point position)
        {
            if (this.IsInWidget(position) && this.Visible)
                return this;
            return null;
        }
    }

    public class ContainerWidget : Widget
    {
        public List<Widget> SubWidgets { get; set; }
        public ContainerWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.SubWidgets = new List<Widget>();
            XElement subWidget = e.Element("SubWidgets").FirstNode as XElement;
            while (subWidget != null)
            {
                this.SubWidgets.Add(Widget.ParseWidget(subWidget, this));

                subWidget = subWidget.NextNode as XElement;
            }
        }
        public ContainerWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.SubWidgets = new List<Widget>();
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            XElement subwidgets = new XElement("SubWidgets");
            foreach (Widget widget in this.SubWidgets)
            {
                subwidgets.Add(widget.GetXml());
            }
            xe.Add(subwidgets);
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            return $"CppLogic.UI.ContainerWidgetCreateContainerWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
        }
        internal override string GetLuaAssert()
        {
            string r = base.GetLuaAssert();
            foreach (Widget w in SubWidgets)
                r += w.GetLuaAssert();
            return r;
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string s = base.GetLuaData(ignorebef);
            foreach (Widget w in SubWidgets)
                s += w.GetLuaData(true);
            return s;
        }

        public override void DrawWidget(Graphics g, float zoom, PointF origin)
        {
            base.DrawWidget(g, zoom, origin);
            PointF newOrigin = new PointF(this.PositionAndSize.X * zoom + origin.X, this.PositionAndSize.Y * zoom + origin.Y);
            for (int i = this.SubWidgets.Count - 1; i >= 0; i--)
            {
                if (SubWidgets[i].Visible)
                    SubWidgets[i].DrawWidget(g, zoom, newOrigin);
            }
        }
        public override Widget GetWidgetUnderPosition(Point position)
        {
            Widget widget = null;
            if (this.Visible)
            {
                int index = 0;
                while (widget == null && index < this.SubWidgets.Count)
                {
                    widget = this.SubWidgets[index].GetWidgetUnderPosition(position);
                    index++;
                }
                if (widget == null)
                    widget = base.GetWidgetUnderPosition(position);
            }
            return widget;
        }
        public override void ShowWidget(Graphics g)
        {
            g.DrawRectangle(Pens.Red, this.CurrentPositionAndSize.X, this.CurrentPositionAndSize.Y, this.CurrentPositionAndSize.Width, this.CurrentPositionAndSize.Height);
        }
    }

    public class SpecialBonusBaseShitWidget : ContainerWidget
    {
        public SpecialBonusBaseShitWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
        }
        public SpecialBonusBaseShitWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement("CurrentRootWidget", this.SubWidgets.Count < 1 ? "" : this.SubWidgets[0].Name));
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            throw new InvalidOperationException("cannot create root widget");
        }

    }

    public class StaticWidget : Widget
    {
        // widget bg
        public Texture Background { get; set; }
        public StaticWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.Background = new Texture(e.Element("BackgroundMaterial"));
        }
        public StaticWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.Background = new Texture("", new RectangleF(0, 0, 1, 1), Color.Black);
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement("BackgroundMaterial", Background.ToXml()));
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            return $"CppLogic.UI.ContainerWidgetCreateStaticWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string escapedname = $"\"{Name}\"";
            string s = base.GetLuaData(ignorebef);
            s += Background.ToLua(escapedname, 0);
            return s;
        }
        public override void DrawWidget(Graphics g, float zoom, PointF origin)
        {
            base.DrawWidget(g, zoom, origin);
            this.Background.DrawTexture(g, this.CurrentPositionAndSize);
        }
    }

    public abstract class ButtonWidget : Widget
    {
        public bool Disabled { get; set; }
        public bool HighLighted { get; set; }
        public string LuaCommand { get; set; }
        public S5String ShortCutString { get; set; }
        public Texture ButtonNormal { get; set; }
        public Texture ButtonHover { get; set; }
        public Texture ButtonPressed { get; set; }
        public Texture ButtonDisabled { get; set; }
        public Texture ButtonHighlighted { get; set; }
        public Tooltip ToolTipHelper { get; set; }
        public WidgetUpdate Update { get; set; }
        public ButtonWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            XElement buttonHelper = e.Element("ButtonHelper");
            this.Disabled = bool.Parse(buttonHelper.Element("DisabledFlag").Value);
            this.HighLighted = bool.Parse(buttonHelper.Element("HighLightedFlag").Value);
            this.LuaCommand = buttonHelper.Element("ActionFunction").Element("LuaCommand").Value;
            this.ShortCutString = new S5String(buttonHelper.Element("ShortCutString"));
            IEnumerable<XElement> buttons = e.Elements("Materials");
            this.ButtonNormal = new Texture(buttons.ElementAt(0));
            this.ButtonHover = new Texture(buttons.ElementAt(1));
            this.ButtonPressed = new Texture(buttons.ElementAt(2));
            this.ButtonDisabled = new Texture(buttons.ElementAt(3));
            this.ButtonHighlighted = new Texture(buttons.ElementAt(4));
            this.ToolTipHelper = new Tooltip(e.Element("ToolTipHelper"));
            this.Update = new WidgetUpdate(e);
        }
        public ButtonWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.Disabled = false;
            this.HighLighted = false;
            this.LuaCommand = "";
            this.ShortCutString = new S5String("", "New Button");
            this.ButtonNormal = new Texture("", new RectangleF(0, 0, 1, 1), Color.FromArgb(100, 155, 155, 155));
            this.ButtonHover = new Texture("", new RectangleF(0, 0, 1, 1), Color.FromArgb(100, 155, 155, 155));
            this.ButtonPressed = new Texture("", new RectangleF(0, 0, 1, 1), Color.FromArgb(100, 155, 155, 155));
            this.ButtonDisabled = new Texture("", new RectangleF(0, 0, 1, 1), Color.FromArgb(100, 155, 155, 155));
            this.ButtonHighlighted = new Texture("", new RectangleF(0, 0, 1, 1), Color.FromArgb(100, 155, 155, 155));
            this.ToolTipHelper = new Tooltip(false, new S5String("", "New Button"), "", false, "");
            this.Update = new WidgetUpdate("", true);
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement[] {
                new XElement("ButtonHelper", new XElement[] {
                    new XElement("DisabledFlag", this.Disabled.ToString()),
                    new XElement("HighLightedFlag", this.HighLighted.ToString()),
                    new XElement("ActionFunction",
                        new XElement("LuaCommand", this.LuaCommand)
                    ),
                    new XElement("ShortCutString", this.ShortCutString.ToXml()),
                }),
                new XElement("Materials", this.ButtonNormal.ToXml()),
                new XElement("Materials", this.ButtonHover.ToXml()),
                new XElement("Materials", this.ButtonPressed.ToXml()),
                new XElement("Materials", this.ButtonDisabled.ToXml()),
                new XElement("Materials", this.ButtonHighlighted.ToXml()),
                new XElement("ToolTipHelper", this.ToolTipHelper.ToXml()),
            });
            xe.Add(this.Update.ToXml());
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            throw new InvalidOperationException("cannot create base button widget");
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string escapedname = $"\"{Name}\"";
            string s = base.GetLuaData(ignorebef);
            s += $"XGUIEng.DisableButton({escapedname}, {(Disabled ? "1" : "0")})\n";
            s += $"XGUIEng.HighLightButton({escapedname}, {(HighLighted ? "1" : "0")})\n";
            if (LuaCommand.Length > 0 && !LuaCommand.StartsWith("--"))
                s += $"CppLogic.UI.ButtonOverrideActionFunc({escapedname}, function() {LuaCommand} end)\n";
            if (ShortCutString.RawString.Length > 0)
                s += $"CppLogic.UI.ButtonSetShortcutString({escapedname}, \"{ShortCutString.RawString}\", false)\n";
            else if (ShortCutString.StringTableKey.Length > 0)
                s += $"CppLogic.UI.ButtonSetShortcutString({escapedname}, \"{ShortCutString.StringTableKey}\", true)\n";
            s += ButtonNormal.ToLua(escapedname, 0);
            s += ButtonHover.ToLua(escapedname, 1);
            s += ButtonPressed.ToLua(escapedname, 2);
            s += ButtonDisabled.ToLua(escapedname, 3);
            s += ButtonHighlighted.ToLua(escapedname, 4);
            s += ToolTipHelper.ToLua(escapedname);
            s += Update.ToLua(escapedname);
            return s;
        }

        public override void DrawWidget(Graphics g, float zoom, PointF origin)
        {
            base.DrawWidget(g, zoom, origin);
            this.ButtonNormal.DrawTexture(g, this.CurrentPositionAndSize);
        }
    }

    public class GfxButtonWidget : ButtonWidget
    {
        public Texture IconMaterial { get; set; } // use?
        public GfxButtonWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.IconMaterial = new Texture(e.Element("IconMaterial"));
        }
        public GfxButtonWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.IconMaterial = new Texture("", new RectangleF(0, 0, 1, 1), Color.FromArgb(100, 155, 155, 155));
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement("IconMaterial", this.IconMaterial.ToXml()));
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            return $"CppLogic.UI.ContainerWidgetCreateGFXButtonWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string escapedname = $"\"{Name}\"";
            string s = base.GetLuaData(ignorebef);
            // maybe iconmaterial
            return s;
        }
        public override void DrawWidget(Graphics g, float zoom, PointF origin)
        {
            base.DrawWidget(g, zoom, origin);
            this.IconMaterial.DrawTexture(g, this.CurrentPositionAndSize);
        }
    }

    public class TextButtonWidget : ButtonWidget
    {
        public S5Writing ButtonText { get; set; }
        public TextButtonWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.ButtonText = new S5Writing(e.Element("StringHelper"));
        }
        public TextButtonWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.ButtonText = new S5Writing("data\\menu\\fonts\\standard12.met", new S5String("", "@center New Button"), 0f, Color.White);
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement("StringHelper", this.ButtonText.ToXml()));
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            return $"CppLogic.UI.ContainerWidgetCreateTextButtonWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string escapedname = $"\"{Name}\"";
            string s = base.GetLuaData(ignorebef);
            s += ButtonText.ToLua(escapedname);
            return s;
        }
        public override void DrawWidget(Graphics g, float zoom, PointF origin)
        {
            base.DrawWidget(g, zoom, origin);
            this.ButtonText.DrawString(g, this.CurrentPositionAndSize, zoom);
        }
    }

    public class StaticTextWidget : Widget
    {
        public Texture Background { get; set; }
        public S5Writing Text { get; set; }
        public WidgetUpdate Update { get; set; }
        public int FirstLineToPrint { get; set; }
        public int NumberOfLinesToPrint { get; set; }
        public float LineDistanceFactor { get; set; }
        public StaticTextWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.Background = new Texture(e.Element("BackgroundMaterial"));
            this.Text = new S5Writing(e.Element("StringHelper"));
            this.Update = new WidgetUpdate(e);
            this.FirstLineToPrint = int.Parse(e.Element("FirstLineToPrint").Value);
            this.NumberOfLinesToPrint = int.Parse(e.Element("NumberOfLinesToPrint").Value);
            this.LineDistanceFactor = float.Parse(e.Element("LineDistanceFactor").Value);
        }
        public StaticTextWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.Background = new Texture("", new RectangleF(0, 0, 1, 1), Color.FromArgb(100, 155, 155, 155));
            this.Text = new S5Writing("data\\menu\\fonts\\standard12.met", new S5String("", "@center New Text"), 0f, Color.White);
            this.Update = new WidgetUpdate("", true);
            this.FirstLineToPrint = 0;
            this.NumberOfLinesToPrint = 0;
            this.LineDistanceFactor = 0f;
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement[] {
                new XElement("BackgroundMaterial", this.Background.ToXml()),
                new XElement("StringHelper", this.Text.ToXml()),
            });
            xe.Add(this.Update.ToXml());
            xe.Add(new XElement[] {
                new XElement("FirstLineToPrint", this.FirstLineToPrint.ToString()),
                new XElement("NumberOfLinesToPrint", this.NumberOfLinesToPrint.ToString()),
                new XElement("LineDistanceFactor", this.LineDistanceFactor.ToString())
            });
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            return $"CppLogic.UI.ContainerWidgetCreateStaticTextWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string escapedname = $"\"{Name}\"";
            string s = base.GetLuaData(ignorebef);
            s += Background.ToLua(escapedname, 0);
            s += Text.ToLua(escapedname);
            s += Update.ToLua(escapedname);
            s += $"XGUIEng.SetLinesToPrint({escapedname}, {FirstLineToPrint}, {NumberOfLinesToPrint})\n";
            s += $"CppLogic.UI.StaticTextWidgetSetLineDistanceFactor({escapedname}, {LineDistanceFactor})\n";
            return s;
        }

        public override void DrawWidget(Graphics g, float zoom, PointF origin)
        {
            base.DrawWidget(g, zoom, origin);
            this.Background.DrawTexture(g, this.CurrentPositionAndSize);
            this.Text.DrawString(g, this.CurrentPositionAndSize, zoom);
        }
    }

    public class CustomWidget : Widget
    {
        public string CustomClassName { get; set; }
        public int IntegerUserVariable0DefaultValue { get; set; }
        public int IntegerUserVariable1DefaultValue { get; set; }
        public int IntegerUserVariable2DefaultValue { get; set; }
        public int IntegerUserVariable3DefaultValue { get; set; }
        public int IntegerUserVariable4DefaultValue { get; set; }
        public int IntegerUserVariable5DefaultValue { get; set; }
        public string StringUserVariable0DefaultValue { get; set; }
        public string StringUserVariable1DefaultValue { get; set; }
        public CustomWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.CustomClassName = e.Element("CustomClassName").Value;
            this.IntegerUserVariable0DefaultValue = int.Parse(e.Element("IntegerUserVariable0DefaultValue").Value);
            this.IntegerUserVariable1DefaultValue = int.Parse(e.Element("IntegerUserVariable1DefaultValue").Value);
            this.IntegerUserVariable2DefaultValue = int.Parse(e.Element("IntegerUserVariable2DefaultValue").Value);
            this.IntegerUserVariable3DefaultValue = int.Parse(e.Element("IntegerUserVariable3DefaultValue").Value);
            this.IntegerUserVariable4DefaultValue = int.Parse(e.Element("IntegerUserVariable4DefaultValue").Value);
            this.IntegerUserVariable5DefaultValue = int.Parse(e.Element("IntegerUserVariable5DefaultValue").Value);
            this.StringUserVariable0DefaultValue = e.Element("StringUserVariable0DefaultValue").Value;
            this.StringUserVariable1DefaultValue = e.Element("StringUserVariable1DefaultValue").Value;
        }
        public CustomWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.CustomClassName = "EGUIX::CVideoPlaybackCustomWidget";
            this.IntegerUserVariable0DefaultValue = 0;
            this.IntegerUserVariable1DefaultValue = 0;
            this.IntegerUserVariable2DefaultValue = 0;
            this.IntegerUserVariable3DefaultValue = 0;
            this.IntegerUserVariable4DefaultValue = 0;
            this.IntegerUserVariable5DefaultValue = 0;
            this.StringUserVariable0DefaultValue = "";
            this.StringUserVariable1DefaultValue = "";
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement[] {
                new XElement("CustomClassName", this.CustomClassName),
                new XElement("IntegerUserVariable0DefaultValue", this.IntegerUserVariable0DefaultValue.ToString()),
                new XElement("IntegerUserVariable1DefaultValue", this.IntegerUserVariable1DefaultValue.ToString()),
                new XElement("IntegerUserVariable2DefaultValue", this.IntegerUserVariable2DefaultValue.ToString()),
                new XElement("IntegerUserVariable3DefaultValue", this.IntegerUserVariable3DefaultValue.ToString()),
                new XElement("IntegerUserVariable4DefaultValue", this.IntegerUserVariable4DefaultValue.ToString()),
                new XElement("IntegerUserVariable5DefaultValue", this.IntegerUserVariable5DefaultValue.ToString()),
                new XElement("StringUserVariable0DefaultValue", this.StringUserVariable0DefaultValue),
                new XElement("StringUserVariable1DefaultValue", this.StringUserVariable1DefaultValue)
            });
            return xe;
        }

        public class CustomWidgetOptions
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

        public static Dictionary<string, CustomWidgetOptions> KnownWidgetTypes = new Dictionary<string, CustomWidgetOptions>();
        static CustomWidget()
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
            KnownWidgetTypes["CppLogic::Mod::UI::AutoScrollCustomWidget"] = new CustomWidgetOptions()
            {
                IntUserVar0 = "int spacing",
                StringUserVar0 = "slider widget",
                StringUserVar1 = "scrollable widget (optional)",
                SaveForExport = true,
            };
            KnownWidgetTypes["CppLogic::Mod::UI::TextInputCustomWidget"] = new CustomWidgetOptions()
            {
                IntUserVar0 = "int mode 0->normal, 1->password, 2->int, 3->double",
                IntUserVar1 = "bool fireCancelEvent",
                StringUserVar0 = "event func (text, widgetid, event) event: 0->confirm, 1->cancel",
                StringUserVar1 = "font (optional)",
                SaveForExport = true,
            };
            KnownWidgetTypes["CppLogic::Mod::UI::FreeCamCustomWidget"] = new CustomWidgetOptions()
            {
                IntUserVar0 = "int scroll speed",
                SaveForExport = true,
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
            KnownWidgetTypes["EGUIX::CScrollBarButtonCustomWidget"] = new CustomWidgetOptions()
            {
                StringUserVar0 = "confirm callback (value, widgetId)",
                StringUserVar1 = "slider gfx source name",
                SaveForExport = false,
            };
        }

        public static CustomWidgetOptions TryGet(string className)
        {
            if (KnownWidgetTypes.TryGetValue(className, out CustomWidgetOptions result))
                return result;
            return null;
        }

        protected override string GetLuaCreator(string parent, string befo)
        {
            var o = TryGet(CustomClassName);
            if (o == null || !o.SaveForExport)
                MessageBox.Show($"Warning: CustomWidget {Name} of type {CustomClassName} in export, make sure this works properly.\nLook at the CppLogic.UI.ContainerWidgetCreateCustomWidgetChild documentation for more info.", "CustomWidget export");
            return $"CppLogic.UI.ContainerWidgetCreateCustomWidgetChild(\"{parent}\", \"{Name}\", \"{CustomClassName}\", {befo}, {IntegerUserVariable0DefaultValue}, {IntegerUserVariable1DefaultValue}, {IntegerUserVariable2DefaultValue}, {IntegerUserVariable3DefaultValue}, {IntegerUserVariable4DefaultValue}, {IntegerUserVariable5DefaultValue}, \"{StringUserVariable0DefaultValue.Replace("\\", "\\\\")}\", \"{StringUserVariable1DefaultValue.Replace("\\", "\\\\")}\")\n";
        }
    }

    public class ProgressBarWidget : Widget
    {
        public Texture Background { get; set; }
        public WidgetUpdate Update { get; set; }
        public float ProgressBarValue { get; set; }
        public float ProgressBarLimit { get; set; }
        public float ProgressRatio
        {
            get
            {
                float progressRatio = this.ProgressBarLimit / this.ProgressBarValue;
                if (float.IsNaN(progressRatio))
                    progressRatio = 1;
                return progressRatio;
            }
        }
        public ProgressBarWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.Background = new Texture(e.Element("BackgroundMaterial"));
            this.Update = new WidgetUpdate(e);
            this.ProgressBarValue = float.Parse(e.Element("ProgressBarValue").Value);
            this.ProgressBarLimit = float.Parse(e.Element("ProgressBarLimit").Value);
        }
        public ProgressBarWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.Background = new Texture("", new RectangleF(0, 0, 1, 1), Color.White);
            this.Update = new WidgetUpdate("", true);
            this.ProgressBarValue = 50f;
            this.ProgressBarLimit = 100f;
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement("BackgroundMaterial", this.Background.ToXml()));
            xe.Add(this.Update.ToXml());
            xe.Add(new XElement[] {
                new XElement("ProgressBarValue", this.ProgressBarValue.ToString()),
                new XElement("ProgressBarLimit", this.ProgressBarLimit.ToString())
            });
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            return $"CppLogic.UI.ContainerWidgetCreateProgressBarWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string escapedname = $"\"{Name}\"";
            string s = base.GetLuaData(ignorebef);
            s += Background.ToLua(escapedname, 0);
            s += $"XGUIEng.SetProgressBarValues({escapedname}, {ProgressBarValue}, {ProgressBarLimit})\n";
            s += Update.ToLua(escapedname);
            return s;
        }

        public override void DrawWidget(Graphics g, float zoom, PointF origin)
        {
            base.DrawWidget(g, zoom, origin);

            float xOrig = this.Background.TexturePosAndSize.X;
            float widthOrig = this.Background.TexturePosAndSize.Width;
            float newX = this.Background.TexturePosAndSize.X + (1 - this.Background.TexturePosAndSize.Width / this.ProgressRatio);
            float newWidth = this.Background.TexturePosAndSize.Width / this.ProgressRatio;
            this.Background.TexturePosAndSize = new RectangleF(newX, this.Background.TexturePosAndSize.Y, newWidth, this.Background.TexturePosAndSize.Height);
            this.Background.DrawTexture(g, new RectangleF(this.CurrentPositionAndSize.Location, new SizeF(this.CurrentPositionAndSize.Width / this.ProgressRatio, this.CurrentPositionAndSize.Height)));
            this.Background.TexturePosAndSize = new RectangleF(xOrig, this.Background.TexturePosAndSize.Y, widthOrig, this.Background.TexturePosAndSize.Height);
        }
    }

    public class PureTooltipWidget : Widget
    {
        public Tooltip Tooltip { get; set; }
        public PureTooltipWidget(XElement e, ContainerWidget parentNode)
            : base(e, parentNode)
        {
            this.Tooltip = new Tooltip(e.Element("ToolTipHelper"));
        }
        public PureTooltipWidget(ContainerWidget parentNode, RectangleF positionAndSize)
            : base(parentNode, positionAndSize)
        {
            this.Tooltip = new Tooltip(false, new S5String("", "New Tooltip"), "", false, "");
        }
        public override XElement GetXml()
        {
            XElement xe = base.GetXml();
            xe.Add(new XElement("ToolTipHelper", this.Tooltip.ToXml()));
            return xe;
        }
        protected override string GetLuaCreator(string parent, string befo)
        {
            return $"CppLogic.UI.ContainerWidgetCreatePureTooltipWidgetChild(\"{parent}\", \"{Name}\", {befo})\n";
        }
        internal override string GetLuaData(bool ignorebef)
        {
            string escapedname = $"\"{Name}\"";
            string s = base.GetLuaData(ignorebef);
            s += Tooltip.ToLua(escapedname);
            return s;
        }
    }

    public class WidgetType
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string OptionsPage { get; set; }
        public WidgetType(string classId, string className, string optionsPage)
        {
            this.ClassId = classId;
            this.ClassName = className;
            this.OptionsPage = optionsPage;
        }
    }
}
