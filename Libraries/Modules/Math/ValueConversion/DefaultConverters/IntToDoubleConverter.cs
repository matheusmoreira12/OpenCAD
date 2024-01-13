namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class IntToDoubleConverter : ValueConverter<int, double>
    {
        public override double Convert(int value) => value;

        public override int ConvertBack(double value) => (int)value;

		public override ValueConverterDirection AllowedDirections => ValueConverterDirection.Both;
	}
}
