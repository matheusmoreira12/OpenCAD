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

        public Line(Point? start = null, Scalar? length = null, Scalar? angle = null, Point? end = null)
        {
            StartPoint = start;
            Length = length;
            RotationAngle = angle;
            EndPoint = end;
        }
    }
}