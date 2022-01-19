using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class ThermalReliefOptions
    {
        public static readonly ThermalReliefOptions Default = new ThermalReliefOptions(
            Scalar.Parse("0deg"),
            4,
            Scalar.Parse("10mil"));

        public ThermalReliefOptions(Scalar spokeAngle, int spokeCount, Scalar spokeWidth)
        {
            SpokeAngle = spokeAngle;
            SpokeCount = spokeCount;
            SpokeWidth = spokeWidth;
        }

        public readonly Scalar SpokeAngle;

        public readonly int SpokeCount;

        public readonly Scalar SpokeWidth;
    }
}