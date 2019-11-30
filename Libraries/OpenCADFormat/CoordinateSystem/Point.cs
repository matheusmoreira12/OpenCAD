using System;
using System.Xml.Serialization;
using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.Measures.Math;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public struct Point
    {
        public static readonly Point Zero = new Point(Scalar.Zero, Scalar.Zero);

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

        public static Scalar Distance(Point a, Point b, Unit outUnit)
        {
            Size difference = (a - b).ConvertTo(outUnit);

            return MathExtension.SquareRoot(difference.Width.Square() + difference.Height.Square());
        }

        public static Scalar Angle(Point a, Point b)
        {
            Size difference = a - b;

            double angleRad = Math.Atan2(difference.Width.ConvertTo(null).Amount, difference.Height.ConvertTo(null).Amount);

            return new Scalar(angleRad, Unit.Parse("rad"));
        }

        public static Scalar Angle(Point a, Point o, Point b) => Angle(a, o) + Angle(o, b);

        public Point(Scalar x, Scalar y)
        {
            X = x;
            Y = y;
        }

        public Size Subtract(Point other) => Subtract(this, other);

        public Point Subtract(Size other) => Subtract(this, other);

        public Point Negate() => Negate(this);

        public Point Add(Point other) => Add(this, other);

        public Point Add(Size other) => Add(this, other);

        public Point Divide(double other) => Divide(this, other);

        public Scalar Angle(Point other) => Angle(this, other);

        public Point ConvertTo(Unit unit) => new Point(X.ConvertTo(unit), Y.ConvertTo(unit));

        public Point ConvertToPixels() => new Point(X.ConvertTo(Unit.Parse("px_x")), Y.ConvertTo(Unit.Parse("px_y")));

        public Scalar X { get; }

        public Scalar Y { get; }
    }
}
