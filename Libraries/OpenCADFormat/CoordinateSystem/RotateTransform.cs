
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public class RotateTransform : Transform
    {
        public Point Center { get; }
        public Measurement Angle { get; }

        public RotateTransform(Point center, Measurement angle)
        {
            Validation.Expect(Quantities.PlaneAngle, angle);

            Center = center;
            Angle = angle;
        }
    }
}
