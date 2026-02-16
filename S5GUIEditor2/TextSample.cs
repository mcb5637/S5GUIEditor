using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace S5GUIEditor2;

internal class TextSample : Control
{
    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<TextSample, string>(nameof(Text));
    public static readonly StyledProperty<RWFont?> FontProperty = AvaloniaProperty.Register<TextSample, RWFont?>(nameof(Font));
    public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<TextSample, Color>(nameof(Color));
    public static readonly StyledProperty<Color> BGColorProperty = AvaloniaProperty.Register<TextSample, Color>(nameof(BGColor));

    static TextSample()
    {
        AffectsRender<TextSample>(TextProperty, FontProperty, ColorProperty, BGColorProperty);
        AffectsRender<TextSample>(TextProperty, FontProperty);
    }
    
    internal string Text {
        get => GetValue(TextProperty);
        set
        {
            SetValue(TextProperty, value);
            InvalidateVisual();
        }
    }
    internal RWFont? Font {
        get => GetValue(FontProperty);
        set
        {
            SetValue(FontProperty, value);
            InvalidateVisual();
        }
    }
    internal Color Color {
        get => GetValue(ColorProperty);
        set
        {
            SetValue(ColorProperty, value);
            InvalidateVisual();
        }
    }
    internal Color BGColor {
        get => GetValue(BGColorProperty);
        set
        {
            SetValue(BGColorProperty, value);
            InvalidateVisual();
        }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (Font == null)
            return new Size(32, 32);
        var r = new Size(double.Min(availableSize.Width, Font.GetLength(Text, new Point(1,1))), 
            double.Min(availableSize.Height, Font.Height));
        return r;
    }

    public sealed override void Render(DrawingContext context)
    {
        if (Font == null)
            return;
        context.Custom(new CustomRender()
        {
            Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height),
            Text = Text,
            Font = Font,
            C = Color,
            Background = BGColor,
        });
    }

    private class CustomRender : ICustomDrawOperation
    {
        public required Rect Bounds { get; init; }
        internal required string Text { get; init; }
        internal required Color C { get; init; }
        internal required Color Background { get; init; }
        internal required RWFont Font { get; init; }
        
        public bool Equals(ICustomDrawOperation? other) => false;

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
            
            canvas.DrawRect(Bounds.ToSKRect(), new SKPaint()
            {
                Color = Background.ToSKColor(),
                Style = SKPaintStyle.Fill,
            });
            Font.Render(canvas, Text, Bounds.TopLeft, new Point(1,1), C);
        }
    }
}