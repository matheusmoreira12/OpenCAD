using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public abstract class UnitNode
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Symbol;

        [XmlAttribute]
        public string UISymbol;
    }
}