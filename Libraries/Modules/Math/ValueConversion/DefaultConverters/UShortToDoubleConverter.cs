namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class UShortToDoubleConverter : ValueConverter<ushort, double>
    {
        public override double Convert(ushort value) => value;

        public override ushort ConvertBack(double value) => (ushort)value;
    }
}
