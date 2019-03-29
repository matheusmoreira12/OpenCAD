using OpenCAD.OpenCADFormat.Measures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class RoutingLayer: Layer
    {
        [XmlAttribute]
        public Measurement CopperPullBack;

        [XmlElement]
        public ThermalReliefOptions ThermalRelief;

        [XmlElement]
        public CopperTheevingOptions CopperTheeving;
    }
}
