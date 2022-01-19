using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class CopperBalancingOptions
    {
        public readonly CopperBalancingPatternType PatternType;

        public readonly Scalar PatternSize;

        public CopperBalancingOptions(CopperBalancingPatternType patternType, Scalar patternSize)
        {
            PatternType = patternType;
            PatternSize = patternSize;
        }
    }
}