using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class DerivedQuantityEntry: QuantityEntry
    {
        [XmlAttribute]
        public string Dimension;

        public override void Assimilate(Quantity value)
        {
            throw new System.NotImplementedException();
        }

        public override Quantity Resolve()
        {
            throw new System.NotImplementedException();
        }
    }
}