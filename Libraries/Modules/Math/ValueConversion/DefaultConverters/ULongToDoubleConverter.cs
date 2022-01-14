namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class ULongToDoubleConverter : ValueConverter<ulong, double>
    {
        public override double Convert(ulong value) => value;

        public override ulong ConvertBack(double value) => (ulong)value;
    }
}
