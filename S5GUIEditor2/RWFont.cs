using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using SkiaSharp;

namespace S5GUIEditor2;

// ReSharper disable once InconsistentNaming
internal class RWFont
{
    internal required SKImage Image { get; init; }
    internal required Rect[] Positions { get; init; }
    internal double Height => this.Positions['X'].Height * Image.Height;

    internal static RWFont? Load(string path, ImageCache c)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        string resolvedPath = c.S!.ResolveS5Path(path);
        using var iter = File.ReadLines(resolvedPath).GetEnumerator();
        if (!iter.MoveNext())
            return null; // metrics
        if (!iter.MoveNext())
            return null;
        string imagePath = Path.GetDirectoryName(path.Replace('\\', '/'))!.Replace('/', '\\') + "\\" + iter.Current;
        SKImage? img = new[]{".dds", ".png"}.Select(x => c.Get(imagePath + x)).FirstOrDefault(x => x != null);
        if (img == null)
            return null;
        if (!iter.MoveNext())
            return null; // 0
        Rect[] rects = new Rect[256];
        while (iter.MoveNext())
        {
            string[] l = iter.Current.Split(['\t', ' '], StringSplitOptions.RemoveEmptyEntries);
            if (l.Length < 5)
                continue;
            
            int ascii = int.Parse(l[0]);
            if (ascii >= 256)
                continue;

            var x = int.Parse(l[1]) - 1;
            var y = int.Parse(l[2]);
            rects[ascii] = new Rect(x / (double)img.Width,
                y / (double)img.Height,
                (1+int.Parse(l[3]) - x) / (double)img.Width,
                (1+int.Parse(l[4]) - y) / (double)img.Height
            );
        }

        return new RWFont()
        {
            Image = img,
            Positions = rects,
        };
    }

    internal Point Render(SKCanvas canvas, string s, Point origin, Point scale, Color color)
    {
        foreach (char c in s)
        {
            if (c >= 256)
                continue;
            Rect src = Positions[c];
            Rect dst = new Rect(origin.X, origin.Y, src.Width * Image.Width * scale.X, src.Height * Image.Height * scale.Y);
            
            TextureView.DoRender(canvas, src, Image, color, dst, false);
            origin = new Point(origin.X + dst.Width, origin.Y);
        }

        return origin;
    }

    internal double GetLength(string s, Point scale)
    {
        double x = 0;
        foreach (char c in s)
            x += Positions[c].Width;
        return x * scale.X * Image.Width;
    }
}