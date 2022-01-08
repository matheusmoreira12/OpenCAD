using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public static class FillStyles
    {
        public static readonly FillStyle None = null;
        public static readonly FillStyle Solid = new SolidFillStyle();
        public static readonly FillStyle Hatched = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("0deg"), new[] {1, 6}) });
        public static readonly FillStyle Hatched90Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("90deg"), new[] {1, 6}) });
        public static readonly FillStyle Hatched45Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("45deg"), new[] {1, 6}) });
        public static readonly FillStyle Hatched135Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("135deg"), new[] {1, 6}) });
        public static readonly FillStyle Crossed = HatchFillStyle.Combine((HatchFillStyle)Hatched,
            (HatchFillStyle)Hatched90Deg);
        public static readonly FillStyle Crossed45Deg = HatchFillStyle.Combine((HatchFillStyle)Hatched45Deg,
            (HatchFillStyle)Hatched135Deg);
    }
}