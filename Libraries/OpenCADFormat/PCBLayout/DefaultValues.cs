using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    static class DefaultValues
    {
        public static readonly Measurement COPPER_PULLBACK = new Measurement(50, Units.Length.Mil);

        //Thermal Relief
        public static readonly Measurement THERMAL_RELIEF_SPOKE_ANGLE = new Measurement(0, Units.PlaneAngle.Degree);
        public const int THERMAL_RELIEF_SPOKE_COUNT = 4;
        public static readonly Measurement THERMAL_RELIEF_SPOKE_WIDTH = new Measurement(10, Units.Length.Mil);

        //Copper Thieving
        public const CopperThievingPatternType COPPER_THIEVING_PATTERN_TYPE = CopperThievingPatternType.Round;
        public static readonly Measurement COPPER_THIEVING_PATTERN_SIZE = new Measurement(50, Units.Length.Mil);
        public const double COPPER_THIEVING_PATTERN_DENSITY = .25;
    }
}
