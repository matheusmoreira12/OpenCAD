using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    static class DefaultValues
    {
        public static readonly Scalar COPPER_PULLBACK = new Scalar(50, Units.Length.Mil);

        //Thermal Relief
        public static readonly Scalar THERMAL_RELIEF_SPOKE_ANGLE = new Scalar(0, Units.PlaneAngle.Degree);
        public const int THERMAL_RELIEF_SPOKE_COUNT = 4;
        public static readonly Scalar THERMAL_RELIEF_SPOKE_WIDTH = new Scalar(10, Units.Length.Mil);

        //Copper Thieving
        public const CopperThievingPatternType COPPER_THIEVING_PATTERN_TYPE = CopperThievingPatternType.Round;
        public static readonly Scalar COPPER_THIEVING_PATTERN_SIZE = new Scalar(50, Units.Length.Mil);
        public const double COPPER_THIEVING_PATTERN_DENSITY = .25;
    }
}
