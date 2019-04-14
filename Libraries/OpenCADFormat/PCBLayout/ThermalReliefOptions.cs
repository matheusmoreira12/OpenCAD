using System;
using System.Xml.Serialization;
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    [Serializable]
    public class ThermalReliefOptions
    {
        [XmlAttribute]
        public Measurement Angle = DefaultValues.THERMAL_RELIEF_SPOKE_ANGLE;

        [XmlAttribute]
        public int SpokeCount = DefaultValues.THERMAL_RELIEF_SPOKE_COUNT;

        [XmlAttribute]
        public Measurement SpokeWidth = DefaultValues.THERMAL_RELIEF_SPOKE_WIDTH;
    }
}