using OpenCAD.OpenCADFormat.Measures;
using System;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public struct Point
    {
        public static Size Subtract(Point a, Point b) => new Size(a.X.Subtract(b.X), a.Y.Subtract(b.Y));
        public static Size Subtract(Point a, Size b) => new Size(a.X.Subtract(b.Width), a.Y.Subtract(b.Height));
        public static Point Add(Point a, Point b) => new Point(a.X.Add(b.X), a.Y.Add(b.Y));
        public static Point Add(Point a, Size b) => new Point(a.X.Add(b.Width), a.Y.Add(b.Height));
        public static (double, double) Divide(Point a, Size b) => (a.X / b.Width, a.Y / b.Width);

        public static Size operator -(Point a, Point b) => Subtract(a, b);
        public static Size operator -(Point a, Size b) => Subtract(a, b);
        public static Point operator +(Point a, Point b) => Add(a, b);
        public static Point operator +(Point a, Size b) => Add(a, b);
        public static (double, double) operator /(Point a, Size b) => Divide(a, b);

        public static Measurement<Measures.Quantities.Length> Distance(Point a, Point b,
            Unit<Measures.Quantities.Length> unit)
        {
            Size difference = (a - b).ConvertTo(unit);
            double distance = Math.Sqrt(Math.Pow(difference.Width.Amount, 2) + Math.Pow(difference.Height.Amount, 2));

            return new Measurement<Measures.Quantities.Length>(distance, unit);
        }

        public static Measurement<Measures.Quantities.PlaneAngle> Angle(Point a, Point b)
        {
            var difference = (a - b);
            double angleRad = Math.Atan2(difference.Width.AsDouble(), difference.Height.AsDouble());

            return new Measurement<Measures.Quantities.PlaneAngle>(angleRad, Measures.Quantities.PlaneAngle.Radian);
        }

        public static Measurement<Measures.Quantities.PlaneAngle> Angle(Point a, Point o, Point b) =>
            Angle(a, o) + Angle(o, b);

        public Point(Measurement<Measures.Quantities.Length> x, Measurement<Measures.Quantities.Length> y)
        {
            X = x;
            Y = y;
        }

        public Measurement<Measures.Quantities.Length> Distance(Point b, Unit<Measures.Quantities.Length> unit) =>
            Distance(this, b, unit);
        public Measurement<Measures.Quantities.PlaneAngle> Angle(Point b) => Angle(this, b);

        public Point ConvertTo(Unit<Measures.Quantities.Length> unit) => new Point(X.ConvertTo(unit), Y.ConvertTo(unit));
        public Point ConvertTo(PrefixedUnit<Measures.Quantities.Length> prefixedUnit) =>
            new Point(X.ConvertTo(prefixedUnit), Y.ConvertTo(prefixedUnit));

        public Measurement<Measures.Quantities.Length> X { get; set; }
        public Measurement<Measures.Quantities.Length> Y { get; set; }
    }

    public struct Size
    {
        public static Size Add(Size a, Size b) => new Size(a.Width + b.Width, a.Height + b.Height);
        public static Size Subtract(Size a, Size b) => new Size(a.Width - b.Width, a.Height - b.Height);
        public static (double, double) Divide(Size a, Size b) => (a.Width / b.Width, a.Height / b.Height);

        public static Size operator +(Size a, Size b) => Add(a, b);
        public static Size operator -(Size a, Size b) => Subtract(a, b);
        public static (double, double) operator /(Size a, Size b) => Divide(a, b);

        public Size(Measurement<Measures.Quantities.Length> width, Measurement<Measures.Quantities.Length> height)
        {
            Width = width;
            Height = height;
        }

        public Size ConvertTo(Unit<Measures.Quantities.Length> unit) => new Size(Width.ConvertTo(unit),
            Height.ConvertTo(unit));
        public Size ConvertTo(PrefixedUnit<Measures.Quantities.Length> prefixedUnit) =>
            new Size(Width.ConvertTo(prefixedUnit), Height.ConvertTo(prefixedUnit));

        public Measurement<Measures.Quantities.Length> Width { get; set; }
        public Measurement<Measures.Quantities.Length> Height { get; set; }
    }

    public abstract class Transform
    {

    }

    public class TranslateTransform : Transform
    {
        public Point Offset { get; private set; }

        public TranslateTransform(Point offset)
        {
            Offset = offset;
        }
    }

    public class RotateTransform : Transform
    {
        public Point Center { get; private set; }
        public Measurement<Measures.Quantities.PlaneAngle> Angle { get; private set; }

        public RotateTransform(Point center, Measurement<Measures.Quantities.PlaneAngle> angle)
        {
            Center = center;
            Angle = angle;
        }
    }
}
