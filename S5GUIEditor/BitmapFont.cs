using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using System.Drawing.Imaging;

namespace S5GUIEditor
{
    public class BitmapFont
    {
        protected Image fontImage;
        protected RectangleF[] charPositions;
        public float Height
        {
            get { return this.charPositions['X'].Height; }
        }

        protected BitmapFont(Image fontImage, string[] metricsFile)
        {
            this.fontImage = fontImage;
            this.charPositions = new RectangleF[256];

            for (int line = 4; line < metricsFile.Length; line++)
            {
                string[] pieces = metricsFile[line].Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (pieces.Length < 5)
                    continue;

                int ascii = int.Parse(pieces[0]);
                if (ascii >= 256)
                    break;

                RectangleF charPos = new RectangleF();

                charPos.X = int.Parse(pieces[1])-1;
                charPos.Y = int.Parse(pieces[2]);
                charPos.Width = 1+int.Parse(pieces[3]) - charPos.X;
                charPos.Height = 1+int.Parse(pieces[4]) - charPos.Y;

                this.charPositions[ascii] = charPos;
            }

        }

        public static BitmapFont CreateFromMetrics(string path)
        {
            string[] allLines = File.ReadAllLines(path);

            string imagePath = Path.GetDirectoryName(path) + "\\" + allLines[1];
            if (Path.GetExtension(imagePath) == "")
            {
                bool found = false;
                foreach (string ext in new string[] { ".dds", ".png" })
                {
                    string testPath = imagePath + ext;
                    if (File.Exists(testPath))
                        imagePath = testPath;
                    found = true;
                    break;
                }
                if (!found)
                    return null;
            }
            Image img = ImageCache.LoadImage(imagePath);
            if (img == null)
                return null;

            return new BitmapFont(img, allLines);
        }
        public void DrawString(Graphics g, PointF origin, float zoom, string text)
        {
            this.DrawString(g, origin, zoom, text, new ImageAttributes());
        }

        public void DrawString(Graphics g, PointF origin, float zoom, string text, ImageAttributes ia)
        {
            char[] chars = text.ToCharArray();

            foreach (char c in chars)
            {
                RectangleF srcRect = this.charPositions[c];
                RectangleF dstRect = new RectangleF(origin.X, origin.Y, srcRect.Width * zoom, srcRect.Height * zoom);

                PointF[] para = new PointF[]
                {
                    new PointF(dstRect.X,   dstRect.Y),                       //UL corner
                    new PointF(dstRect.X+dstRect.Width, dstRect.Y),        //UR
                    new PointF(dstRect.X,   dstRect.Y+dstRect.Height)      //LL
                };
                g.DrawImage(this.fontImage, para, srcRect, GraphicsUnit.Pixel, ia);

                //g.DrawImage(this.fontImage, dstRect, srcRect, GraphicsUnit.Pixel);
                origin.X += dstRect.Width-zoom;
            }
        }

        public float GetLength(string text)
        {
            float sum = 0;
            foreach (char c in text.ToCharArray())
                sum += this.charPositions[c].Width-1f;
            return sum;
        }
    }
}
