using OpenCAD.OpenCADFormat.Serialization;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class MetricPrefixNode
    {
        [XmlAttribute]
        public string Name = null;

        [XmlAttribute]
        public string Symbol = null;

        [XmlAttribute]
        public string UISymbol = null;

        [XmlAttribute]
        public double Multiplier = 0;
    }
}