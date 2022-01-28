using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Polygon : Shape
    {
        public readonly Point? CenterPoint;

        public readonly Scalar? RotationAngle;

        public readonly int? SideCount;

        public readonly Point[] Points;

        public Polygon(Point? centerPoint = null, Scalar? rotationAngle = null, int? sideCount = null, Point[] points = null)
        {
            CenterPoint = centerPoint;
            RotationAngle = rotationAngle;
            SideCount = sideCount;
            Points = points;
        }
    }
}