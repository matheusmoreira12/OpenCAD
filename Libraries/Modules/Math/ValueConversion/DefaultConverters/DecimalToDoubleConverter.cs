namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class DecimalToDoubleConverter : ValueConverter<decimal, double>
    {
        public override double Convert(decimal value) => (double)value;

        public override decimal ConvertBack(double value) => (decimal)value;

		public override ValueConverterDirection AllowedDirections => ValueConverterDirection.Both;
	}
}
