namespace OpenCAD.Modules.Math.Numerics
{
    public struct ImaginaryNumber : IComplexNumber
    {
        public static explicit operator ImaginaryNumber(decimal value) => new ImaginaryNumber((double)value);

        public static explicit operator ImaginaryNumber(double value) => new ImaginaryNumber(value);

        public static explicit operator ImaginaryNumber(float value) => new ImaginaryNumber(value);

        public static explicit operator decimal(ImaginaryNumber value) => (decimal)value.PrimitiveForm;

        public static explicit operator double(ImaginaryNumber value) => value.PrimitiveForm;

        public static explicit operator float(ImaginaryNumber value) => (float)value.PrimitiveForm;

        public static explicit operator ComplexNumber(ImaginaryNumber value) => new ComplexNumber((RealNumber)0.0, value);

        private ImaginaryNumber(double primitiveValue)
        {
            PrimitiveForm = primitiveValue;
        }

        private readonly double PrimitiveForm;

        public RealNumber RealPart => (RealNumber)0.0;

        public ImaginaryNumber ImaginaryPart => this;
    }
}
