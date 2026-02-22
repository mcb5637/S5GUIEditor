using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using S5GUIEditor2.Widgets;
using SkiaSharp;

namespace S5GUIEditor2;

internal class MaterialGridSelector : Control
{
    public static readonly StyledProperty<CMaterial?> MaterialProperty = AvaloniaProperty.Register<MaterialGridSelector, CMaterial?>(nameof(Image));
    // only for re-render
    public static readonly StyledProperty<SKImage?> ImageProperty = AvaloniaProperty.Register<MaterialGridSelector, SKImage?>(nameof(Image));
    public static readonly StyledProperty<Rect> RectangleProperty = AvaloniaProperty.Register<MaterialGridSelector, Rect>(nameof(Rectangle));

    static MaterialGridSelector()
    {
        AffectsRender<MaterialGridSelector>(MaterialProperty, ImageProperty, RectangleProperty);
        AffectsMeasure<MaterialGridSelector>(ImageProperty);
    }
    
    public CMaterial? Material
    {
        get => GetValue(MaterialProperty);
        set
        {
            SetValue(MaterialProperty, value);
            InvalidateVisual();
        }
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
    public Rect Rectangle
    {
        get => GetValue(RectangleProperty);
        set
        {
            SetValue(RectangleProperty, value);
            InvalidateVisual();
        }
    }
    
    private (int, int) HoveredGridCell = (-1, -1);

    private Rect RenderBounds => Image == null
        ? new Rect(0, 0, Bounds.Width, Bounds.Height)
        : new Rect(0, 0, double.Min(Bounds.Width, Image.Width), double.Min(Bounds.Height, Image.Height));
    
    private bool GridActive {
        get
        {
            if (Material == null || Material.Image == null)
                return false;
            var nxd = Material.GridNumX;
            var nyd = Material.GridNumY;
            return Math.Abs(double.Floor(nxd) - nxd) < 0.001 && Math.Abs(double.Floor(nyd) - nyd) < 0.001;
        }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (Image == null)
            return new Size(32, 32);
        var r = new Size(double.Min(availableSize.Width, Image.Width), 
            double.Min(availableSize.Height, Image.Height));
        return r;
    }

    private (int, int) GetPointerGrid(Point pos)
    {
        if (Material == null)
            return (-1, -1);
        var bounds = RenderBounds;
        var nxd = Material.GridNumX;
        var nyd = Material.GridNumY;
        var x = pos.X / bounds.Width * nxd;
        var y = pos.Y / bounds.Height * nyd;
        return ((int)x, (int)y);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (Material == null || !GridActive)
            return;
        var n = GetPointerGrid(e.GetPosition(this));
        if (HoveredGridCell == n)
            return;
        HoveredGridCell = n;
        InvalidateVisual();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        if (Material == null || !GridActive)
            return;
        var (x, y) = GetPointerGrid(e.GetPosition(this));
        Material.GridX = x;
        Material.GridY = y;
        InvalidateVisual();
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        HoveredGridCell = (-1, -1);
        InvalidateVisual();
    }

    public sealed override void Render(DrawingContext context)
    {
        if (Bounds.Width == 0 || Bounds.Height == 0)
            return;
        context.Custom(new CustomRender()
        {
            Material = Material,
            Bounds = RenderBounds,
            WhiteBG = true,
            GridActive = GridActive,
            HoveredGridCell = HoveredGridCell,
        });
    }

    private class CustomRender : ICustomDrawOperation
    {
        internal required CMaterial? Material { get; init; }
        internal required bool WhiteBG { get; init; }
        internal required bool GridActive { get; init; }
        internal required (int, int) HoveredGridCell { get; init; }
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
            TextureView.DoRender(canvas, CMaterial.UvFull.ToRect, Material?.Image, Colors.White, Bounds, WhiteBG);
            if (!GridActive || Material == null || Material.Image == null)
                return;
            var nxd = Material.GridNumX;
            var nyd = Material.GridNumY;
            int nx = (int)nxd;
            int ny = (int)nyd;
            var sx = Bounds.Width / nx;
            var sy = Bounds.Height / ny;
            SKPaint paint = new()
            {
                Color = SKColors.Green,
                Style = SKPaintStyle.Stroke,
            };
            for (int x = 0; x <= nx; ++x)
            {
                var px = (float)(sx * x);
                canvas.DrawLine(new SKPoint(px, 0), new SKPoint(px, (float)Bounds.Height), paint);
            }
            for (int y = 0; y <= ny; ++y)
            {
                var py = (float)(sy * y);
                canvas.DrawLine(new SKPoint(0, py), new SKPoint((float)Bounds.Width, py), paint);
            }

            var (selx, sely) = HoveredGridCell;
            if (selx >= 0 && selx < nx && sely >= 0 && sely < ny)
            {
                paint.Color = SKColors.Blue;
                var px = (float)(sx * selx);
                var py = (float)(sy * sely);
                canvas.DrawRect(px, py, (float)sx, (float)sy, paint);
            }
            {
                paint.Color = SKColors.Red;
                var px = (float)(sx * Material.GridX);
                var py = (float)(sy * Material.GridY);
                canvas.DrawRect(px, py, (float)sx, (float)sy, paint);
            }
        }

        public bool Equals(ICustomDrawOperation? other) => false;
    }
}