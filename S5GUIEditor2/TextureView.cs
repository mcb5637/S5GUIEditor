using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using S5GUIEditor2.Widgets;
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
        set
        {
            SetValue(ImageProperty, value);
            InvalidateVisual();
        }
    }

    public RectangleF Rectangle
    {
        get => GetValue(RectangleProperty);
        set
        {
            SetValue(RectangleProperty, value);
            InvalidateVisual();
        }
    }

    public Color Color
    {
        get => GetValue(ColorProperty);
        set
        {
            SetValue(ColorProperty, value); 
            InvalidateVisual();
        }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (Image == null)
            return new Size(32, 32);
        var s = Image.Size;
        var r = new Size(double.Min(availableSize.Width, Rectangle.Width * s.Width), 
            double.Min(availableSize.Height, Rectangle.Height * s.Height));
        return r;
    }
    
    public sealed override void Render(DrawingContext context)
    {
        context.Custom(new CustomRender()
        {
            C = Color,
            Image = Image,
            Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height),
            Source = Rectangle,
            WhiteBG = true,
        });
    }

    private class CustomRender : ICustomDrawOperation
    {
        internal required Color C { get; init; }
        internal required Bitmap? Image { get; init; }
        internal required RectangleF Source { get; init; }
        internal required bool WhiteBG { get; init; }
        public required Rect Bounds { get; init; }
        
        public void Dispose()
        {
        }

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            DoRender(context, Source, Image, C, Bounds, WhiteBG);
        }

        public bool Equals(ICustomDrawOperation? other) => false;
    }

    internal static void DoRender(ImmediateDrawingContext context, RectangleF source, Bitmap? image, Color color, Rect bounds, bool whiteBG)
    {
        ISkiaSharpApiLeaseFeature? leaseFeature = context.TryGetFeature(typeof(ISkiaSharpApiLeaseFeature)) as ISkiaSharpApiLeaseFeature;
        if (leaseFeature == null)
        {
            return;
        }

        using ISkiaSharpApiLease lease = leaseFeature.Lease();
        SKCanvas canvas = lease.SkCanvas;

        if (whiteBG)
        {
            canvas.DrawRect(bounds.ToSKRect(), new SKPaint()
            {
                Color = SKColors.White,
                Style = SKPaintStyle.Fill,
            });
        }

        if (image != null)
        {
            var imageSize = image.Size;
            Rect src = new(source.X * imageSize.Width,
                source.Y * imageSize.Height, source.Width * imageSize.Width,
                source.Height * imageSize.Height);
            
            MemoryStream s = new();
            image.Save(s);
            using SKBitmap bitmap = SKBitmap.Decode(s.ToArray());
            SKImage img = SKImage.FromBitmap(bitmap);

            using SKPaint paint = new();
            paint.ImageFilter = SKImageFilter.CreateColorFilter(SKColorFilter.CreateBlendMode(color.ToSKColor(),
                SKBlendMode.Modulate));
            paint.FilterQuality = SKFilterQuality.High;

            canvas.DrawImage(img, src.ToSKRect(), bounds.ToSKRect(), paint);
        }
        else
        {
            canvas.DrawRect(bounds.ToSKRect(), new SKPaint()
            {
                Color = color.ToSKColor(),
                Style = SKPaintStyle.Fill,
            });
        }
    }
}