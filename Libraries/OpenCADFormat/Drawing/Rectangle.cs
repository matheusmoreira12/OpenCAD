using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum RectangleType { TwoPoint, ThreePoint, Centered }

    public class Rectangle : Shape
    {
        public static Rectangle CreateTwoPoint(Point start, Point end) => new Rectangle()
        {
            Start = start,
            End = end
        };

        public static Rectangle CreateThreePoint(Point start, Point end, Point control) => new Rectangle()
        {
            Start = start,
            End = end,
            Control = control
        };

        public static Rectangle CreateCentered(Point center, Point end) => new Rectangle()
        {
            Center = center,
            End = end
        };

        private Rectangle() { }

        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point Control { get; private set; }
        public Point Center { get; private set; }
    }
}