using System;
using System.Xml.Serialization;
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public struct Point
    {
        public static readonly Point Zero = new Point(Measurement.Zero, Measurement.Zero);

        public static Size Subtract(Point a, Point b) => new Size(a.X - b.X, a.Y - b.Y);
        public static Point Subtract(Point a, Size b) => new Point(a.X - b.Width, a.Y - b.Height);
        public static Point Negate(Point a) => new Point(-a.X, -a.Y);
        public static Point Add(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        public static Point Add(Point a, Size b) => new Point(a.X + b.Width, a.Y + b.Height);
        public static Point Divide(Point a, double b) => new Point(a.X / b, a.Y / b);

        public static Size operator -(Point a, Point b) => Subtract(a, b);
        public static Point operator -(Point a, Size b) => Subtract(a, b);
        public static Point operator -(Point a) => Negate(a);
        public static Point operator +(Point a, Point b) => Add(a, b);
        public static Point operator +(Point a, Size b) => Add(a, b);
        public static Point operator /(Point a, double b) => Divide(a, b);

        public static Measurement Distance(Point a, Point b, Unit outUnit)
        {
            Size difference = (a - b).ConvertTo(outUnit);

            double distance = Math.Sqrt(Math.Pow(difference.Width.Amount, 2)
                + Math.Pow(difference.Height.Amount, 2));

            return new Measurement(distance, outUnit);
        }

        public static Measurement Angle(Point a, Point b)
        {
            Size difference = (a - b);

            double angleRad = Math.Atan2(difference.Width.GetAbsoluteAmount(), difference.Height.GetAbsoluteAmount());

            return new Measurement(angleRad, Units.PlaneAngle.Radian);
        }

        public static Measurement Angle(Point a, Point o, Point b) =>
            Angle(a, o) + Angle(o, b);

        public Point(Measurement x, Measurement y)
        {
            Validation.Expect(Quantities.Length, x);
            Validation.Expect(Quantities.Length, y);

            X = x;
            Y = y;
        }

        public Size ConvertTo(Unit unit) => new Size(X.ConvertToUnit(unit), Y.ConvertToUnit(unit));

        [XmlAttribute]
        public Measurement X { get; set; }

        [XmlAttribute]
        public Measurement Y { get; set; }
    }
}
