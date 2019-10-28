namespace OpenCAD.OpenCADFormat.Measures
{
    static class UnitMath
    {
        public static Unit Power(this Unit unit, double exponent) => new ExponentiatedUnit(unit, exponent).Collapse();

        public static Unit Square(this Unit unit) => Power(unit, 2);

        public static Unit Cube(this Unit unit) => Power(unit, 3);

        public static Unit SquareRoot(this Unit unit) => Power(unit, 1.0 / 2.0);

        public static Unit CubicRoot(this Unit unit) => Power(unit, 1.0 / 3.0);

        public static Unit Invert(this Unit unit) => Power(unit, -1);

        public static Unit Multiply(this Unit a, Unit b) => new ComposedUnit(a, b).Collapse();

        public static Unit Multiply(this Unit a, MetricPrefix b) => new PrefixedUnit(a, b);

        public static Unit Divide(this Unit a, Unit b)
        {
            if (a is null)
                return Invert(b);

            return Multiply(a, Invert(b));
        }
    }
}
