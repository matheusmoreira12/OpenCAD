using OpenCAD.OpenCADFormat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class MetricSystemEntry: IResolvable<MetricSystem>
    {
        [XmlAttribute]
        public string Name;

        [XmlArray("Quantities")]
        [XmlElement("BaseQuantity", typeof(BaseQuantityEntry))]
        [XmlElement("DerivedQuantity", typeof(BaseQuantityEntry))]
        public QuantityEntry[] Quantities;

        [XmlArray("Prefixes")]
        [XmlElement("Prefix", typeof(MetricPrefixEntry))]
        public MetricPrefixEntry[] Prefixes;

        public MetricSystem Resolve()
        {
            var quantities = Quantities.Select(q => q.Resolve());
            var prefixes = Prefixes.Select(p => p.Resolve());
            var metricSystem = new MetricSystem(Name, quantities.ToArray(), prefixes.ToArray());
            return metricSystem;
        }

        public void Assimilate(MetricSystem value)
        {
            throw new NotImplementedException();
        }
    }
}
