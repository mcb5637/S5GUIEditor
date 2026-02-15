using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using S5GUIEditor2.Widgets;
using SkiaSharp;

namespace S5GUIEditor2;

internal class GUIRender : Control
{
    public static readonly StyledProperty<CBaseWidget?> RootWidgetProperty = AvaloniaProperty.Register<TextureView, CBaseWidget?>(nameof(RootWidget));
    
    internal CBaseWidget? RootWidget {
        get => GetValue(RootWidgetProperty);
        set
        {
            SetValue(RootWidgetProperty, value);
            InvalidateVisual();
        }
    }

    public sealed override void Render(DrawingContext context)
    {
        if (RootWidget == null)
            return;
        context.Custom(new CustomRender()
        {
            Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height),
            RootWidget = RootWidget,
        });
    }

    private class CustomRender : ICustomDrawOperation
    {
        public required Rect Bounds { get; init; }
        public required CBaseWidget RootWidget { get; init; }
        private Point Scale { get; set; }

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            if (context.TryGetFeature(typeof(ISkiaSharpApiLeaseFeature)) is not ISkiaSharpApiLeaseFeature leaseFeature)
            {
                return;
            }

            using ISkiaSharpApiLease lease = leaseFeature.Lease();
            SKCanvas canvas = lease.SkCanvas;

            Scale = new Point(Bounds.Width / RootWidget.PositionAndSize.Width, Bounds.Height / RootWidget.PositionAndSize.Height);
            
            DoRender(canvas, new Point(Bounds.X, Bounds.Y), RootWidget);
        }

        private void DoRender(SKCanvas canvas, Point topLeft, CBaseWidget wid)
        {
            if (!wid.Visible)
                return;
            Rect r = new Rect(topLeft.X + wid.PositionAndSize.X * Scale.X,
                topLeft.Y + wid.PositionAndSize.Y * Scale.Y,
                wid.PositionAndSize.Width * Scale.X,
                wid.PositionAndSize.Height * Scale.Y
            );
            if (wid.RendererMaterial != null)
            {
                TextureView.DoRender(canvas, wid.RendererMaterial.TextureCoordinates, wid.RendererMaterial.Image, wid.RendererMaterial.Color, r, false);
            }

            if (wid is CContainerWidget cw)
            {
                foreach (CBaseWidget child in cw.WidgetListHandler.SubWidgets.Reverse())
                    DoRender(canvas, r.TopLeft, child);
            }
        }

        public bool Equals(ICustomDrawOperation? other) => false;

        public void Dispose()
        {
        }
    }
}