namespace OpenCAD.Modules.Math.Numerics
{
    public struct ComplexNumber : IComplexNumber
    {
        public ComplexNumber(RealNumber realPart, ImaginaryNumber imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }

        public RealNumber RealPart { get; }

        public ImaginaryNumber ImaginaryPart { get; }
    }
}
