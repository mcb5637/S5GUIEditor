using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
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
    public static readonly StyledProperty<SKImage?> ImageProperty = AvaloniaProperty.Register<TextureView, SKImage?>(nameof(Image));
    public static readonly StyledProperty<RectangleF> RectangleProperty = AvaloniaProperty.Register<TextureView, RectangleF>(nameof(Rectangle));
    public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<TextureView, Color>(nameof(Color));

    static TextureView()
    {
        AffectsRender<TextureView>(ImageProperty, RectangleProperty, ColorProperty);
        AffectsMeasure<TextureView>(RectangleProperty, ImageProperty);
    }
    
    public SKImage? Image
    {
        get => GetValue(ImageProperty);
        set
        {
            SetValue(ImageProperty, value);
            InvalidateVisual();
        }
    }

    private void OnPropertyChangedRedraw(object? sender, PropertyChangedEventArgs e)
    {
        InvalidateVisual();
    }

    public RectangleF Rectangle
    {
        get => GetValue(RectangleProperty);
        set
        {
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            Rectangle?.PropertyChanged -= OnPropertyChangedRedraw;
            SetValue(RectangleProperty, value);
            value.PropertyChanged += OnPropertyChangedRedraw;
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
        var r = new Size(double.Min(availableSize.Width, Rectangle.Width * Image.Width), 
            double.Min(availableSize.Height, Rectangle.Height * Image.Height));
        return r;
    }
    
    public sealed override void Render(DrawingContext context)
    {
        if (Bounds.Width == 0 || Bounds.Height == 0)
            return;
        context.Custom(new CustomRender()
        {
            C = Color,
            Image = Image,
            Bounds = Image == null ? new Rect(0, 0, Bounds.Width, Bounds.Height) :
                new Rect(0, 0, double.Min(Bounds.Width, Image.Width), double.Min(Bounds.Height, Image.Height)),
            Source = Rectangle,
            WhiteBG = true,
        });
    }

    private class CustomRender : ICustomDrawOperation
    {
        internal required Color C { get; init; }
        internal required SKImage? Image { get; init; }
        internal required RectangleF Source { get; init; }
        internal required bool WhiteBG { get; init; }
        public required Rect Bounds { get; init; }
        
        public void Dispose()
        {
        }

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            if (context.TryGetFeature(typeof(ISkiaSharpApiLeaseFeature)) is not ISkiaSharpApiLeaseFeature leaseFeature)
                return;

            using ISkiaSharpApiLease lease = leaseFeature.Lease();
            SKCanvas canvas = lease.SkCanvas;
            DoRender(canvas, Source.ToRect, Image, C, Bounds, WhiteBG);
        }

        public bool Equals(ICustomDrawOperation? other) => false;
    }

    internal static void DoRender(SKCanvas canvas, Rect source, SKImage? image, Color color, Rect bounds, bool whiteBG)
    {
        SKRect b = bounds.ToSKRect();
        if (whiteBG)
        {
            canvas.DrawRect(b, new SKPaint()
            {
                Color = SKColors.White,
                Style = SKPaintStyle.Fill,
            });
        }

        if (image != null)
        {
            Rect src = new(source.X * image.Width,
                source.Y * image.Height, source.Width * image.Width,
                source.Height * image.Height);
            
            using SKPaint paint = new();
            paint.ImageFilter = SKImageFilter.CreateColorFilter(SKColorFilter.CreateColorMatrix([
                color.R/255.0f, 0, 0, 0, 0,
                0, color.G/255.0f, 0, 0, 0,
                0, 0, color.B/255.0f, 0, 0,
                0, 0, 0, color.A/255.0f, 0,
            ]));
            paint.FilterQuality = SKFilterQuality.High;

            canvas.DrawImage(image, src.ToSKRect(), b, paint);
        }
        else
        {
            canvas.DrawRect(b, new SKPaint()
            {
                Color = color.ToSKColor(),
                Style = SKPaintStyle.Fill,
            });
        }
    }
}