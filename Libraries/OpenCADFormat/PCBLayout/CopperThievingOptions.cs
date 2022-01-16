using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class CopperThievingOptions
    {
        public readonly CopperThievingPatternType PatternType;

        public readonly Scalar PatternSize;

        public readonly double PatternDensity;

        public CopperThievingOptions()
        {
            PatternType = DefaultValues.COPPER_THIEVING_PATTERN_TYPE;
            PatternSize = DefaultValues.COPPER_THIEVING_PATTERN_SIZE;
            PatternDensity = DefaultValues.COPPER_THIEVING_PATTERN_DENSITY;
        }

        public CopperThievingOptions(CopperThievingPatternType patternType, Scalar patternSize, double patternDensity)
        {
            PatternType = patternType;
            PatternSize = patternSize;
            PatternDensity = patternDensity;
        }
    }
}