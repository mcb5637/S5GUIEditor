using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace S5GUIEditor
{
    public static class XmlConverter
    {
        public static RectangleF ToRectangleF(XElement e)
        {
            RectangleF rectangle = new RectangleF();
            rectangle.X = float.Parse(e.Element("X").Value);
            rectangle.Y = float.Parse(e.Element("Y").Value);
            rectangle.Width = float.Parse(e.Element("W").Value);
            rectangle.Height = float.Parse(e.Element("H").Value);
            return rectangle;
        }

        public static XElement[] FromRectangleF(RectangleF rect)
        {
            return new XElement[] {
                new XElement("X", rect.X.ToString()),
                new XElement("Y", rect.Y.ToString()),
                new XElement("W", rect.Width.ToString()),
                new XElement("H", rect.Height.ToString())
            };
        }

        public static Color ToColor(XElement e)
        {
            int r, g, b, a;
            r = int.Parse(e.Element("Red").Value);
            g = int.Parse(e.Element("Green").Value);
            b = int.Parse(e.Element("Blue").Value);
            a = int.Parse(e.Element("Alpha").Value);
            return Color.FromArgb(a, r, g, b);
        }

        public static XElement[] FromColor(Color color)
        {
            return new XElement[] {
                new XElement("Red", color.R.ToString()),
                new XElement("Green", color.G.ToString()),
                new XElement("Blue", color.B.ToString()),
                new XElement("Alpha", color.A.ToString())
            };
        }
    }

    public class WidgetUpdate
    {
        public string LuaUpdateCommand { get; set; }
        public bool UpdateManualFlag { get; set; }
        public WidgetUpdate(string luaUpdateCommand, bool updateManualFlag)
        {
            this.LuaUpdateCommand = luaUpdateCommand;
            this.UpdateManualFlag = updateManualFlag;
        }
        public WidgetUpdate(XElement e)
        {
            this.LuaUpdateCommand = e.Element("UpdateFunction").Element("LuaCommand").Value;
            this.UpdateManualFlag = bool.Parse(e.Element("UpdateManualFlag").Value);
        }

        public XElement[] ToXml()
        {
            return new XElement[] {
                new XElement("UpdateFunction",
                    new XElement("LuaCommand", this.LuaUpdateCommand)
                ),
                new XElement("UpdateManualFlag", this.UpdateManualFlag.ToString())
            };
        }
        public string ToLua(string escapedname)
        {
            string s = $"CppLogic.UI.WidgetSetUpdateManualFlag({escapedname}, {UpdateManualFlag.ToString().ToLower()})\n";
            if (LuaUpdateCommand.Length > 0 && !LuaUpdateCommand.StartsWith("--"))
                s += $"CppLogic.UI.WidgetOverrideUpdateFunc({escapedname}, function() {LuaUpdateCommand} end)\n";
            return s;
        }
    }

    public class Tooltip
    {
        public bool EnabledFlag { get; set; }
        public S5String Text { get; set; }
        public string TargetWidget { get; set; }
        public bool ControlTargetWidgetDisplayState { get; set; }
        public string LuaUpdateCommand { get; set; }
        public Tooltip(bool enabledFlag, S5String text, string targetWidget, bool controlTargetWidgetDisplayState, string luaUpdateCommand)
        {
            this.EnabledFlag = enabledFlag;
            this.Text = text;
            this.TargetWidget = targetWidget;
            this.ControlTargetWidgetDisplayState = controlTargetWidgetDisplayState;
            this.LuaUpdateCommand = luaUpdateCommand;
        }
        public Tooltip(XElement e)
        {
            this.EnabledFlag = bool.Parse(e.Element("ToolTipEnabledFlag").Value);
            this.Text = new S5String(e.Element("ToolTipString"));
            this.TargetWidget = e.Element("TargetWidget").Value;
            this.ControlTargetWidgetDisplayState = bool.Parse(e.Element("ControlTargetWidgetDisplayState").Value);
            this.LuaUpdateCommand = e.Element("UpdateFunction").Element("LuaCommand").Value;
        }

        public XElement[] ToXml()
        {
            return new XElement[] {
                new XElement("ToolTipEnabledFlag", this.EnabledFlag.ToString()),
                new XElement("ToolTipString", this.Text.ToXml()),
                new XElement("TargetWidget", this.TargetWidget),
                new XElement("ControlTargetWidgetDisplayState", this.ControlTargetWidgetDisplayState.ToString()),
                new XElement("UpdateFunction", 
                    new XElement("LuaCommand", this.LuaUpdateCommand)
                )
            };
        }
        public string ToLua(string escapedname)
        {
            string twid;
            if (TargetWidget.Length == 0)
                twid = "nil";
            else
                twid = $"\"{TargetWidget}\"";
            string s = $"CppLogic.UI.WidgetSetTooltipData({escapedname}, {twid}, {ControlTargetWidgetDisplayState.ToString().ToLower()}, {EnabledFlag.ToString().ToLower()})\n";
            if (LuaUpdateCommand.Length > 0 && !LuaUpdateCommand.StartsWith("--"))
                s += $"CppLogic.UI.WidgetOverrideTooltipFunc({escapedname}, function() {LuaUpdateCommand} end)\n";
            if (Text.RawString.Length > 0)
                s += $"CppLogic.UI.WidgetSetTooltipString({escapedname}, \"{Text.RawString}\", false)\n";
            else if (Text.StringTableKey.Length > 0)
                s += $"CppLogic.UI.WidgetSetTooltipString({escapedname}, \"{Text.StringTableKey}\", true)\n";
            return s;
        }
    }

    public class Texture
    {
        public Image TextureImage { get; protected set; }
        protected string texturePath;
        public bool MirrorX = false, MirrorY = false;
        public string TexturePath
        {
            get { return this.texturePath; }
            set
            {
                this.texturePath = value;
                if (value == "")
                {
                    TextureImage = null;
                    return;
                }
                string loadStr = value;
                if (value.Substring(0, 4).ToLower() == "data")
                    loadStr = value.Substring(5);
                string pathToAdd = GlobalSettings.DataPath;
                this.TextureImage = ImageCache.LoadImage(pathToAdd + loadStr);
            }
        }
        public RectangleF TexturePosAndSize { get; set; }
        protected Color rgba;
        protected ImageAttributes colorFilter;
        protected Brush colorBrush;
        public Color RGBA
        {
            get { return this.rgba; }
            set
            {
                this.rgba = value;
                ColorMatrix cm = new ColorMatrix(new float[][]
                {
                    new float[] {value.R/255f, 0, 0, 0, 0},    //r
                    new float[] {0, value.G/255f, 0, 0, 0}, //g
                    new float[] {0, 0, value.B/255f, 0, 0},    //b
                    new float[] {0, 0, 0, value.A/255f, 0},    //a
                    new float[] {0, 0, 0, 0, 1}     //add, 1
                });

                this.colorFilter = new ImageAttributes();
                colorFilter.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                this.colorBrush = new SolidBrush(value);
            }
        }

        public Texture(string texturePath, RectangleF texturePosAndSize, Color rgbaValues)
        {
            this.TexturePath = texturePath;
            this.TexturePosAndSize = texturePosAndSize;
            this.RGBA = rgbaValues;
        }
        public Texture(XElement e)
        {
            this.TexturePath = e.Element("Texture").Value;
            this.TexturePosAndSize = XmlConverter.ToRectangleF(e.Element("TextureCoordinates"));
            if (TexturePosAndSize.Width < 0)
            {
                TexturePosAndSize = DoMirrorX(TexturePosAndSize);
                MirrorX = true;
            }
            if (TexturePosAndSize.Height < 0)
            {
                TexturePosAndSize = DoMirrorY(TexturePosAndSize);
                MirrorY = true;
            }
            this.RGBA = XmlConverter.ToColor(e.Element("Color"));
        }
        public XElement[] ToXml()
        {
            RectangleF r = TexturePosAndSize;
            if (MirrorX)
                r = DoMirrorX(r);
            if (MirrorY)
                r = DoMirrorY(r);
            return new XElement[] {
                new XElement("Texture", this.TexturePath),
                new XElement("TextureCoordinates", XmlConverter.FromRectangleF(r)),
                new XElement("Color", XmlConverter.FromColor(this.RGBA))
            };
        }
        public string ToLua(string escapedname, int i)
        {
            RectangleF r = TexturePosAndSize;
            if (MirrorX)
                r = DoMirrorX(r);
            if (MirrorY)
                r = DoMirrorY(r);
            string s = $"CppLogic.UI.WidgetMaterialSetTextureCoordinates({escapedname}, {i}, {r.X}, {r.Y}, {r.Width}, {r.Height})\n";
            if (TexturePath.Length > 0)
                s += $"XGUIEng.SetMaterialTexture({escapedname}, {i}, \"{TexturePath.Replace("\\", "\\\\")}\")\n";
            s += $"XGUIEng.SetMaterialColor({escapedname}, {i}, {RGBA.R}, {RGBA.G}, {RGBA.B}, {RGBA.A})\n";
            return s;
        }
        public void DrawTexture(Graphics g, RectangleF posAndSize)
        {
            if (this.TextureImage == null)
            {
                g.FillRectangle(this.colorBrush, posAndSize);
            }
            else
            {
                RectangleF srcRect = new RectangleF(this.TexturePosAndSize.X * this.TextureImage.Width, this.TexturePosAndSize.Y * this.TextureImage.Height, this.TexturePosAndSize.Width * this.TextureImage.Width, this.TexturePosAndSize.Height * this.TextureImage.Height);
                if (MirrorX)
                    srcRect = DoMirrorX(srcRect);
                if (MirrorY)
                    srcRect = DoMirrorY(srcRect);
                PointF[] para = new PointF[]
                {
                    new PointF(posAndSize.X,   posAndSize.Y),                       //UL corner
                    new PointF(posAndSize.X+posAndSize.Width, posAndSize.Y),        //UR
                    new PointF(posAndSize.X,   posAndSize.Y+posAndSize.Height)      //LL
                };
                g.DrawImage(this.TextureImage, para, srcRect, GraphicsUnit.Pixel, this.colorFilter);
            }
        }

        public static RectangleF DoMirrorX(RectangleF i)
        {
            i.X += i.Width;
            i.Width = -i.Width;
            return i;
        }
        public static RectangleF DoMirrorY(RectangleF i)
        {
            i.Y += i.Height;
            i.Height = -i.Height;
            return i;
        }
    }


    public class S5String
    {
        public string StringTableKey { get; set; }
        public string RawString { get; set; }

        public S5String(string stringTableKey, string rawString)
        {
            this.StringTableKey = stringTableKey;
            this.RawString = rawString;
        }
        public S5String(XElement e)
        {
            this.StringTableKey = e.Element("StringTableKey").Value;
            this.RawString = e.Element("RawString").Value;
        }

        public XElement[] ToXml()
        {
            return new XElement[]{
                new XElement("StringTableKey", this.StringTableKey),
                new XElement("RawString", this.RawString)
            };
        }
        public string ToLua(string escapedname)
        {
            if (StringTableKey.Length > 0)
                return $"XGUIEng.SetTextKeyName({escapedname}, \"{StringTableKey}\")\n";
            else
                return $"XGUIEng.SetText({escapedname}, \"{RawString}\", 1)\n";
        }
    }

    public class S5Writing
    {
        public BitmapFont Font { get; protected set; }
        protected string fontPath;
        public string FontPath
        {
            get { return this.fontPath; }
            set
            {
                this.fontPath = value;
                if (value == "")
                {
                    this.Font = null;
                    return;
                }
                string loadStr = value;
                if (value.Substring(0, 4).ToLower() == "data")
                    loadStr = value.Substring(5);
                if (Path.GetExtension(loadStr) != "")
                    loadStr = loadStr.Substring(0, loadStr.Length - 4) + ".met";

                this.Font = FontCache.LoadFont(GlobalSettings.DataPath + loadStr);
            }
        }
        public S5String Text { get; set; }
        public float StringFrameDistance { get; set; }
        protected ImageAttributes colorFilter;
        protected Color rgba;
        public Color RGBA
        {
            get { return this.rgba; }
            set
            {
                this.rgba = value;
                ColorMatrix cm = new ColorMatrix(new float[][]
                {
                    new float[] {value.R/255f, 0, 0, 0, 0},    //r
                    new float[] {0, value.G/255f, 0, 0, 0}, //g
                    new float[] {0, 0, value.B/255f, 0, 0},    //b
                    new float[] {0, 0, 0, value.A/255f, 0},    //a
                    new float[] {0, 0, 0, 0, 1}     //add, 1
                });

                this.colorFilter = new ImageAttributes();
                colorFilter.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            }
        }
        public S5Writing(string fontName, S5String text, float stringFrameDistance, Color rgba)
        {
            this.FontPath = fontName;
            this.Text = text;
            this.StringFrameDistance = stringFrameDistance;
            this.RGBA = rgba;
        }
        public S5Writing(XElement e)
        {
            this.FontPath = e.Element("Font").Element("FontName").Value;
            this.Text = new S5String(e.Element("String"));
            this.StringFrameDistance = float.Parse(e.Element("StringFrameDistance").Value);
            this.RGBA = XmlConverter.ToColor(e.Element("Color"));
        }

        public XElement[] ToXml()
        {
            return new XElement[] {
                new XElement("Font", 
                    new XElement("FontName", this.FontPath)
                ),
                new XElement("String", this.Text.ToXml()),
                new XElement("StringFrameDistance", this.StringFrameDistance.ToString()),
                new XElement("Color", XmlConverter.FromColor(this.RGBA))
            };
        }
        public string ToLua(string escapedname)
        {
            string font = FontPath;
            if (!font.StartsWith("data"))
                font = "data\\" + font;
            font = font.Replace("\\", "\\\\");
            string s = $"CppLogic.UI.WidgetSetFont({escapedname}, \"{font}\")\n";
            s += $"CppLogic.UI.WidgetSetStringFrameDistance({escapedname}, {StringFrameDistance})\n";
            s += Text.ToLua(escapedname);
            s += $"XGUIEng.SetTextColor({escapedname}, {RGBA.R}, {RGBA.G}, {RGBA.B}, {RGBA.A})\n";
            return s; 
        }

        public void DrawString(Graphics g, RectangleF posAndSize, float zoom)
        {
            if (this.Font != null)
            {
                posAndSize.Y += (this.StringFrameDistance + 5f) * zoom;

                string drawText = "@center Abc";
                PointF position;
                if (this.Text.RawString != "")
                    drawText = this.Text.RawString;
                if (drawText.Length > 8 && drawText.Substring(0, 8) == "@center ")
                {
                    drawText = drawText.Substring(8);
                    float len = this.Font.GetLength(drawText);
                    position = new PointF(posAndSize.X + posAndSize.Width / 2f - len * zoom / 2f, posAndSize.Y);
                }
                else
                    position = new PointF(posAndSize.X, posAndSize.Y);
                this.Font.DrawString(g, position, zoom, drawText, this.colorFilter);
            }
        }
    }
}
