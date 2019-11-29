using OpenCAD.OpenCADFormat.Serialization;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public abstract class QuantityNode
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Symbol;

        [XmlAttribute]
        public string UISymbol;

        [XmlArray("Units")]
        [XmlElement("Unit", typeof(BaseUnitNode))]
        public BaseUnitNode[] Units;
    }
}