using System;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    [Serializable]
    public class ThermalReliefOptions
    {
        [XmlAttribute]
        public Scalar Angle = DefaultValues.THERMAL_RELIEF_SPOKE_ANGLE;

        [XmlAttribute]
        public int SpokeCount = DefaultValues.THERMAL_RELIEF_SPOKE_COUNT;

        [XmlAttribute]
        public Scalar SpokeWidth = DefaultValues.THERMAL_RELIEF_SPOKE_WIDTH;
    }
}