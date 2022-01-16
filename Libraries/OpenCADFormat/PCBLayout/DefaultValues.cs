using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    static class DefaultValues
    {
        public static readonly Scalar COPPER_PULLBACK = Scalar.Parse("50mil");

        public static readonly Scalar THERMAL_RELIEF_SPOKE_ANGLE = Scalar.Parse("0deg");
        public const int THERMAL_RELIEF_SPOKE_COUNT = 4;
        public static readonly Scalar THERMAL_RELIEF_SPOKE_WIDTH = Scalar.Parse("10mil");

        public const CopperThievingPatternType COPPER_THIEVING_PATTERN_TYPE = CopperThievingPatternType.Round;
        public static readonly Scalar COPPER_THIEVING_PATTERN_SIZE = Scalar.Parse("50mil");
        public const double COPPER_THIEVING_PATTERN_DENSITY = .25;
    }
}
