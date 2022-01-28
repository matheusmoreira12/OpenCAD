
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public sealed class RotateTransform : Transform
    {
        public Point Center { get; }
        public Scalar Angle { get; }

        public RotateTransform(Point center, Scalar angle)
        {
            Center = center;
            Angle = angle;
        }
    }
}
