namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class ShortToDoubleConverter : ValueConverter<short, double>
    {
        public override double Convert(short value) => value;

        public override short ConvertBack(double value) => (short)value;

		public override ValueConverterDirection AllowedDirections => ValueConverterDirection.Both;
	}
}
