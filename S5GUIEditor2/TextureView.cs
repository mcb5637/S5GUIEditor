using System.Drawing;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;
using Color = Avalonia.Media.Color;
using Point = Avalonia.Point;
using Size = Avalonia.Size;

namespace S5GUIEditor2;

internal class TextureView : Control
{
    public static readonly StyledProperty<Bitmap?> ImageProperty = AvaloniaProperty.Register<TextureView, Bitmap?>(nameof(Image));
    public static readonly StyledProperty<RectangleF> RectangleProperty = AvaloniaProperty.Register<TextureView, RectangleF>(nameof(Rectangle));
    public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<TextureView, Color>(nameof(Color));

    public Bitmap? Image
    {
        get => GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public RectangleF Rectangle
    {
        get => GetValue(RectangleProperty);
        set => SetValue(RectangleProperty, value);
    }

    public Color Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (Image == null)
            return availableSize;
        var s = Image.Size;
        var r = new Size(double.Min(availableSize.Width, Rectangle.Width * s.Width), 
            double.Min(availableSize.Height, Rectangle.Height * s.Height));
        return r;
    }
    
    public sealed override void Render(DrawingContext context)
    {
        if (Image == null)
            return;
        var s = Image.Size;
        Rect src = new(Rectangle.X * s.Width,
            Rectangle.Y * s.Height, Rectangle.Width * s.Width,
            Rectangle.Height * s.Height);
        context.Custom(new CustomRender()
        {
            C = Color,
            Image = Image,
            Bounds = new Rect(0, 0, double.Min(src.Width, Bounds.Width), double.Min(src.Height, Bounds.Height)),
            Source = src.ToSKRect(),
        });
    }

    private class CustomRender : ICustomDrawOperation
    {
        internal required Color C { get; init; }
        internal required Bitmap? Image { get; init; }
        internal required SKRect Source { get; init; }
        
        public void Dispose()
        {
        }

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            ISkiaSharpApiLeaseFeature? leaseFeature = context.TryGetFeature(typeof(ISkiaSharpApiLeaseFeature)) as ISkiaSharpApiLeaseFeature;
            if (leaseFeature == null)
            {
                return;
            }

            using ISkiaSharpApiLease lease = leaseFeature.Lease();
            SKCanvas canvas = lease.SkCanvas;
            
            MemoryStream s = new();
            Image!.Save(s);
            using SKBitmap bitmap = SKBitmap.Decode(s.ToArray());
            using SKPaint paint = new();
            
            if (bitmap == null)
            {
                return;
            }
            SKImage img = SKImage.FromBitmap(bitmap);
            paint.ImageFilter = SKImageFilter.CreateColorFilter(SKColorFilter.CreateBlendMode(C.ToSKColor(), SKBlendMode.Modulate));
            paint.FilterQuality = SKFilterQuality.High;
            canvas.DrawImage(img, Source, Bounds.ToSKRect(), paint);
        }

        public required Rect Bounds { get; init; }
        public bool Equals(ICustomDrawOperation? other) => false;
    }
}