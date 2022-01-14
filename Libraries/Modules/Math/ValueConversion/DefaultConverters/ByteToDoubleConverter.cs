namespace OpenCAD.Modules.Math.ValueConversion.DefaultConverters
{
    public sealed class ByteToDoubleConverter : ValueConverter<byte, double>
    {
        public override double Convert(byte value) => value;

        public override byte ConvertBack(double value) => (byte)value;
    }
}
