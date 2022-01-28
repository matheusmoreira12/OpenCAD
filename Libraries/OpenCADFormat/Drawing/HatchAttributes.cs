using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class HatchAttributes
    {
        public HatchAttributes(HatchStyle style, Scalar thickness, Scalar angle, int[] hatchArray)
        {
            Style = style;
            Thickness = thickness;
            Angle = angle;
            HatchArray = hatchArray;
        }

        public HatchStyle Style { get; private set; }
        public Scalar Thickness { get; private set; }
        public Scalar Angle { get; private set; }
        public int[] HatchArray { get; private set; }
    }
}