
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public class RotateTransform : Transform
    {
        public Point Center { get; }
        public Scalar Angle { get; }

        public RotateTransform(Point center, Scalar angle)
        {
            Validation.Expect(Quantities.PlaneAngle, angle);

            Center = center;
            Angle = angle;
        }
    }
}
