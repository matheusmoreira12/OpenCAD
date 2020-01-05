using OpenCAD.OpenCADFormat.CoordinateSystem;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum EllipseType { Centered, TwoPoint, ThreePoint }

    public class Ellipse : Shape
    {
        public static Ellipse CreateCentered(Point center, Size radius, Scalar rotation)
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

        public EllipseType Type;

        public Point Start;
        public bool ShouldSerializeStart => Type == EllipseType.ThreePoint;

        public Point End;
        public bool ShouldSerializeEnd => Type == EllipseType.ThreePoint;

        public Point Control;
        public bool ShouldSerializeControl => Type == EllipseType.ThreePoint;

        public Point Center;
        public bool ShouldSerializeCenter => Type == EllipseType.ThreePoint;

        public Size Radius;
        public bool ShouldSerializeRadius => Type == EllipseType.ThreePoint;

        public Scalar Rotation;
        public bool ShouldSerializeRotation => Type == EllipseType.ThreePoint;
    }
}