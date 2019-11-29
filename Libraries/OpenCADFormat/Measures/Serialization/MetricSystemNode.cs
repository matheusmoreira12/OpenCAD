using OpenCAD.OpenCADFormat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class MetricSystemNode
    {
        [XmlAttribute]
        public string Name;

        [XmlArray("Quantities")]
        [XmlElement("BaseQuantity", typeof(BaseQuantityNode))]
        [XmlElement("DerivedQuantity", typeof(BaseQuantityNode))]
        public QuantityNode[] Quantities = new QuantityNode[0];

        [XmlArray("Units")]
        [XmlElement("BaseUnit", typeof(BaseUnit))]
        [XmlElement("DerivedUnit", typeof(DerivedUnitNode))]
        public BaseUnitNode[] Units = new BaseUnitNode[0];

        [XmlArray("Prefixes")]
        [XmlElement("Prefix", typeof(MetricPrefixNode))]
        public MetricPrefixNode[] Prefixes = new MetricPrefixNode[0];
    }
}
