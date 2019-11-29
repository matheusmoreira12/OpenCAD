using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class DerivedQuantityNode: QuantityNode
    {
        [XmlAttribute]
        public string Dimension = null;
    }
}