using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class ThermalReliefOptions
    {
        public ThermalReliefOptions()
        {
            Angle = DefaultValues.THERMAL_RELIEF_SPOKE_ANGLE;
            SpokeCount = DefaultValues.THERMAL_RELIEF_SPOKE_COUNT;
            SpokeWidth = DefaultValues.THERMAL_RELIEF_SPOKE_WIDTH;
        }

        public ThermalReliefOptions(Scalar angle, int spokeCount, Scalar spokeWidth)
        {
            Angle = angle;
            SpokeCount = spokeCount;
            SpokeWidth = spokeWidth;
        }

        public readonly Scalar Angle;

        public readonly int SpokeCount;

        public readonly Scalar SpokeWidth;
    }
}