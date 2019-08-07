
using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum ArcType { Centered, ThreePoint, CenteredStartSweepAngle }

    public class Arc : Shape
    {
        public static Arc CreateCentered(Point center, Point start, Point end) => new Arc
        {
            Type = ArcType.Centered,
            Center = center,
            Start = start,
            End = end
        };

        public static Arc CreateThreePoint(Point start, Point end, Point control) => new Arc
        {
            Type = ArcType.ThreePoint,
            Start = start,
            End = end,
            Control = control
        };

        public static Arc CreateCenteredStartSweepAngle(Point center, Size radius, Scalar rotation,
            Scalar startAngle, Scalar sweepAngle)
        {
            Validation.Expect(Quantities.PlaneAngle, rotation);

            return new Arc
            {
                Type = ArcType.CenteredStartSweepAngle,
                Center = center,
                Radius = radius,
                Rotation = rotation,
                StartAngle = startAngle,
                SweepAngle = sweepAngle
            };
        }

        private Arc() { }

        public ArcType Type { get; private set; }
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point Control { get; private set; }
        public Point Center { get; private set; }
        public Size Radius { get; private set; }
        public Scalar Rotation { get; private set; }
        public Scalar StartAngle { get; private set; }
        public Scalar SweepAngle { get; private set; }
    }
}