using System;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    [Serializable]
    public class RoutingLayer: Layer
    {
        [XmlAttribute]
        public Scalar CopperPullBack;

        [XmlElement]
        public ThermalReliefOptions ThermalRelief;

        [XmlElement]
        public CopperThievingOptions CopperTheeving;
    }
}
