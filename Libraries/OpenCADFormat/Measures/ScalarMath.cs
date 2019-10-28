using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    static class ScalarMath
    {
        public static Scalar Add(this Scalar a, Scalar b) => new Scalar(a.Amount + Utils.ConvertAmount(b, a.Unit), a.Unit);

        public static Scalar Subtract(this Scalar a, Scalar b) => new Scalar(a.Amount - Utils.ConvertAmount(b, a.Unit), a.Unit);

        public static Scalar Negate(this Scalar a) => new Scalar(-a.Amount, a.Unit);

        public static Scalar Invert(this Scalar a) => new Scalar(1.0 / a.Amount, !a.Unit);

        public static Scalar Multiply(this Scalar a, double b) => new Scalar(a.Amount * b, a.Unit);

        public static Scalar Multiply(this Scalar a, Scalar b) => new Scalar(a.Amount * b.Amount, a.Unit * b.Unit);

        public static Scalar Divide(this Scalar a, double b) => new Scalar(a.Amount / b, a.Unit);

        public static Scalar Divide(this Scalar a, Scalar b) => new Scalar(a.Amount / b.Amount, a.Unit / b.Unit);

        public static Scalar Power(this Scalar a, double b) => new Scalar(Math.Pow(a.Amount, b), a.Unit.Power(b));

        public static Scalar Square(this Scalar a) => Power(a, 2);

        public static Scalar Cube(this Scalar a) => Power(a, 3);

        public static Scalar SquareRoot(this Scalar a) => Power(a, 1.0 / 2.0);

        public static Scalar CubicRoot(this Scalar a) => Power(a, 1.0 / 3.0);

    }
}
