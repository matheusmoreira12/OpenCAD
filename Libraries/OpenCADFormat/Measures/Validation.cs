namespace OpenCAD.OpenCADFormat.Measures
{
    public static class Validation
    {
        public static void Expect(Quantity expected, Quantity actual)
        {
            if (actual == null) return;

            if (expected != actual) throw new WrongUnitQuantityException(expected, actual);
        }

        public static void Expect(Quantity expectedQuantity, Unit unit)
        {
            if (unit == null) return;

            Expect(expectedQuantity, unit.Quantity);
        }

        public static void Expect(Quantity expectedQuantity, Measurement measurement)
        {
            if (measurement == null) return;

            Expect(expectedQuantity, measurement.Unit);
        }
    }
}