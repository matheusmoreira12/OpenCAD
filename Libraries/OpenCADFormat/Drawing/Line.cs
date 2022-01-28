using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Line : Shape
    {
        public readonly Point? StartPoint;

        public readonly Scalar? Length;

        public readonly Scalar? RotationAngle;

        public readonly Point? EndPoint;

        public Line(
            Point? start,
            Scalar? length,
            Scalar? angle,
            Point? end,
            StrokeAttributes stroke,
            FillStyle fill) : base(
                stroke,
                fill)
        {
            StartPoint = start;
            Length = length;
            RotationAngle = angle;
            EndPoint = end;
        }
    }
}