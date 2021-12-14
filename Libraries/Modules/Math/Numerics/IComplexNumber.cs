namespace OpenCAD.Modules.Math.Numerics
{
    public interface IComplexNumber
    {
        public RealNumber RealPart { get; }

        public ImaginaryNumber ImaginaryPart { get; }
    }
}
