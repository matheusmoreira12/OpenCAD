using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public struct StrokeAttributes
    {
        public static readonly StrokeAttributes Default = new StrokeAttributes(StrokeStyles.Solid, Scalar.Parse("10mil"));

        public StrokeAttributes(StrokeStyle style, Scalar thickness)
        {
            Style = style;
            Thickness = thickness;
        }

        public StrokeStyle Style { get; private set; }
        public Scalar Thickness { get; private set; }
    }
}