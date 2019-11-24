
using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum EllipseType { Centered, TwoPoint, ThreePoint }

    public class Ellipse : Shape
    {
        public static Ellipse CreateCentered(Point center, Size radius,
            Scalar rotation)
        {
            return new Ellipse
            {
                Type = EllipseType.Centered,
                Center = center,
                Radius = radius,
                Rotation = rotation
            };
        }

        public static Ellipse CreateTwoPoint(Point start, Point end) => new Ellipse
        {
            Type = EllipseType.TwoPoint,
            Start = start,
            End = end
        };

        public static Ellipse CreateThreePoint(Point start, Point end, Point control) => new Ellipse
        {
            Type = EllipseType.ThreePoint,
            Start = start,
            End = end,
            Control = control
        };

        private Ellipse() { }

        public EllipseType Type { get; private set; }
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point Control { get; private set; }
        public Point Center { get; private set; }
        public Size Radius { get; private set; }
        public Scalar Rotation { get; private set; }
    }
}