using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using S5GUIEditor2.Widgets;

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

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            DoRender(context, new Point(Bounds.X, Bounds.Y), RootWidget);
        }

        private void DoRender(ImmediateDrawingContext context, Point topLeft, CBaseWidget wid)
        {
            Rect r = new Rect(topLeft.X + wid.PositionAndSize.X, topLeft.Y + wid.PositionAndSize.Y, wid.PositionAndSize.Width, wid.PositionAndSize.Height);
            if (wid.RendererMaterial != null)
            {
                TextureView.DoRender(context, wid.RendererMaterial.TextureCoordinates, wid.RendererMaterial.Image, wid.RendererMaterial.Color, r, false);
            }

            if (wid is CContainerWidget cw)
            {
                foreach (CBaseWidget child in cw.WidgetListHandler.SubWidgets)
                    DoRender(context, r.TopLeft, child);
            }
        }

        public bool Equals(ICustomDrawOperation? other) => false;

        public void Dispose()
        {
        }
    }
}