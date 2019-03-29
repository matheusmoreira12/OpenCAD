
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public class RotateTransform : Transform
    {
        public Point Center { get; private set; }
        public Measurement Angle { get; private set; }

        public RotateTransform(Point center, Measurement angle)
        {
            Validation.Expect(Quantities.PlaneAngle, angle);

            Center = center;
            Angle = angle;
        }
    }
}
