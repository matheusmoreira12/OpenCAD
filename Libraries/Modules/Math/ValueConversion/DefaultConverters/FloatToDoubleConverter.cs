namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class FloatToDoubleConverter : ValueConverter<float, double>
    {
        public override double Convert(float value) => value;

        public override float ConvertBack(double value) => (float)value;
    }
}
