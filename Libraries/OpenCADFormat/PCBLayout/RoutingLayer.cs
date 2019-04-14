using System;
using System.Xml.Serialization;
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    [Serializable]
    public class RoutingLayer: Layer
    {
        [XmlAttribute]
        public Measurement CopperPullBack;

        [XmlElement]
        public ThermalReliefOptions ThermalRelief;

        [XmlElement]
        public CopperThievingOptions CopperTheeving;
    }
}
