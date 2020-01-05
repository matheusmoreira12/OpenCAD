using System;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    [Serializable]
    public class CopperThievingOptions
    {
        [XmlAttribute]
        public CopperThievingPatternType PatternType = DefaultValues.COPPER_THIEVING_PATTERN_TYPE;

        [XmlAttribute]
        public Scalar PatternSize = DefaultValues.COPPER_THIEVING_PATTERN_SIZE;

        [XmlAttribute]
        public double PatternDensity = DefaultValues.COPPER_THIEVING_PATTERN_DENSITY;
    }

    public enum CopperThievingPatternType { Round, Square }
}