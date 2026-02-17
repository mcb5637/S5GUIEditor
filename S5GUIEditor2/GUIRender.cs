using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using S5GUIEditor2.Widgets;
using SkiaSharp;

namespace S5GUIEditor2;

internal class GUIRender : Control
{
    public static readonly StyledProperty<CBaseWidget?> RootWidgetProperty = AvaloniaProperty.Register<TextureView, CBaseWidget?>(nameof(RootWidget));
    public static readonly StyledProperty<ObservableCollection<CBaseWidget>?> SelectedWidgetsProperty = AvaloniaProperty.Register<TextureView, ObservableCollection<CBaseWidget>?>(nameof(SelectedWidgets));
    
    internal CBaseWidget? RootWidget {
        get => GetValue(RootWidgetProperty);
        set
        {
            SetValue(RootWidgetProperty, value);
            InvalidateVisual();
        }
    }
    internal ObservableCollection<CBaseWidget>? SelectedWidgets {
        get => GetValue(SelectedWidgetsProperty);
        set
        {
            SetValue(SelectedWidgetsProperty, value);
            InvalidateVisual();
        }
    }

    private static readonly SKImage Background;

    static GUIRender()
    {
        Background = SKImage.FromEncodedData(Assembly.GetExecutingAssembly().GetManifestResourceStream("S5GUIEditor2.resources.bg.png"));
    }
    
    public sealed override void Render(DrawingContext context)
    {
        if (RootWidget == null)
            return;
        context.Custom(new CustomRender()
        {
            Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height),
            RootWidget = RootWidget,
            SelectedWidgets = SelectedWidgets,
            PointedAt = PointedAt,
        });
    }

    private CBaseWidget? GetWidgetAtPos(Point p)
    {
        return RootWidget == null ? null : Search([RootWidget], p);

        static CBaseWidget? Search(IEnumerable<CBaseWidget> w, Point p)
        {
            foreach (CBaseWidget c in w)
            {
                if (!c.Visible)
                    continue;
                Point np = new Point(p.X - c.PositionAndSize.X,  p.Y - c.PositionAndSize.Y);
                if (np.X < 0 || np.Y < 0 || np.X > c.PositionAndSize.Width || np.Y > c.PositionAndSize.Height)
                    continue;
                if (c is CContainerWidget cw)
                {
                    var r = Search(cw.WidgetListHandler.SubWidgets, np);
                    if (r != null)
                        return r;
                }
                return c;
            }
            return null;
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        var n = GetWidgetAtPos(e.GetCurrentPoint(this).Position);
        if (n == null)
            return;
        SelectedWidgets?.Clear();
        SelectedWidgets?.Add(n);
        InvalidateVisual();
    }

    private CBaseWidget? PointedAt = null;
    protected override void OnPointerMoved(PointerEventArgs e)
    {
        var n = GetWidgetAtPos(e.GetCurrentPoint(this).Position);
        if (PointedAt != n)
        {
            PointedAt = n;
            InvalidateVisual();
        }
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        if (PointedAt != null)
        {
            PointedAt = null;
            InvalidateVisual();
        }
    }

    private class CustomRender : ICustomDrawOperation
    {
        public required Rect Bounds { get; init; }
        internal required CBaseWidget RootWidget { get; init; }
        private Point Scale { get; set; }
        internal required ObservableCollection<CBaseWidget>? SelectedWidgets { get; init; }
        internal required CBaseWidget? PointedAt { get; init; }

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            if (context.TryGetFeature(typeof(ISkiaSharpApiLeaseFeature)) is not ISkiaSharpApiLeaseFeature leaseFeature)
                return;

            using ISkiaSharpApiLease lease = leaseFeature.Lease();
            SKCanvas canvas = lease.SkCanvas;

            Scale = new Point((Bounds.Width-2) / RootWidget.PositionAndSize.Width, (Bounds.Height-2) / RootWidget.PositionAndSize.Height);
            
            canvas.DrawRect(Bounds.ToSKRect(), new SKPaint()
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Fill,
            });
            canvas.DrawImage(Background, new Point(Bounds.X+1, Bounds.Y+1).ToSKPoint());
            
            DoRender(canvas, new Point(Bounds.X+1, Bounds.Y+1), RootWidget);
            DoRenderBorder(canvas, new Point(Bounds.X+1, Bounds.Y+1), RootWidget);
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
                TextureView.DoRender(canvas, wid.RendererMaterial.TextureCoordinates.ToRect, wid.RendererMaterial.Image, wid.RendererMaterial.Color, r, false);
            }

            if (wid.TextRender != null)
            {
                var f = wid.TextRender.Font.Font;
                if (f != null)
                {
                    f.Render(canvas, wid.TextRender.String.ToRender, r.TopLeft, Scale, wid.TextRender.Color);
                }
            }

            if (wid is CContainerWidget cw)
            {
                foreach (CBaseWidget child in cw.WidgetListHandler.SubWidgets.Reverse())
                    DoRender(canvas, r.TopLeft, child);
            }
        }

        private void DoRenderBorder(SKCanvas canvas, Point topLeft, CBaseWidget wid)
        {
            Rect r = new Rect(topLeft.X + wid.PositionAndSize.X * Scale.X,
                topLeft.Y + wid.PositionAndSize.Y * Scale.Y,
                wid.PositionAndSize.Width * Scale.X,
                wid.PositionAndSize.Height * Scale.Y
            );
            
            if (wid is CContainerWidget cw)
            {
                foreach (CBaseWidget child in cw.WidgetListHandler.SubWidgets.Reverse())
                    DoRenderBorder(canvas, r.TopLeft, child);
            }

            if (SelectedWidgets?.Contains(wid) ?? false)
            {
                canvas.DrawRect(r.ToSKRect(), new SKPaint()
                {
                    Color = SKColors.Red,
                    Style = SKPaintStyle.Stroke,
                });
            }
            else if (PointedAt == wid)
            {
                canvas.DrawRect(r.ToSKRect(), new SKPaint()
                {
                    Color = SKColors.Blue,
                    Style = SKPaintStyle.Stroke,
                });
            }
        }

        public bool Equals(ICustomDrawOperation? other) => false;

        public void Dispose()
        {
        }
    }
}