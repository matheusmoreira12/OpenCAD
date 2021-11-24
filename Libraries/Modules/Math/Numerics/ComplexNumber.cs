namespace OpenCAD.APIs.Math.Numerics
{
    public struct ComplexNumber
    {
        public readonly RealNumber RealPart;

        public readonly RealNumber ImaginaryPart;

        public ComplexNumber(RealNumber realPart, RealNumber imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }
    }
}
