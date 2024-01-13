namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class LongToDoubleConverter : ValueConverter<long, double>
    {
        public override double Convert(long value) => value;

        public override long ConvertBack(double value) => (long)value;

		public override ValueConverterDirection AllowedDirections => ValueConverterDirection.Both;
	}
}
