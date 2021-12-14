namespace OpenCAD.Modules.Math.Numerics
{
    public struct RealNumber : IComplexNumber
    {
        public static explicit operator RealNumber(decimal value) => new RealNumber((double)value);

        public static explicit operator RealNumber(double value) => new RealNumber(value);

        public static explicit operator RealNumber(float value) => new RealNumber(value);

        public static explicit operator decimal(RealNumber value) => (decimal)value.PrimitiveForm;

        public static explicit operator double(RealNumber value) => value.PrimitiveForm;

        public static explicit operator float(RealNumber value) => (float)value.PrimitiveForm;

        public static explicit operator ComplexNumber(RealNumber value) => new ComplexNumber(value, (ImaginaryNumber)0.0);

        private RealNumber(double primitiveValue)
        {
            PrimitiveForm = primitiveValue;
        }

        private readonly double PrimitiveForm;

        public RealNumber RealPart => this;

        public ImaginaryNumber ImaginaryPart => (ImaginaryNumber)0.0;
    }
}
