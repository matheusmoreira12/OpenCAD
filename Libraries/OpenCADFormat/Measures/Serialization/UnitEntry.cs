using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public abstract class UnitEntry
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Symbol;

        [XmlAttribute]
        public string UISymbol;
    }
}