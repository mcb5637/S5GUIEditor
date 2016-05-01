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
    }

    public class ProgressBarWidget : Widget
    {
        public Texture Background { get; set; }
        public WidgetUpdate Update { get; set; }
        public float ProgressBarValue { get; set; }
        public float ProgressBarLimit { get; set; }
        public float ProgressRatio { 
            get
            { 
                float progressRatio = this.ProgressBarLimit / this.ProgressBarValue;
                if (float.IsNaN(progressRatio))
                    progressRatio = 1;
                return  progressRatio;
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
