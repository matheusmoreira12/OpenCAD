using OpenCAD.OpenCADFormat.Serialization;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public abstract class QuantityNode
    {
        [XmlAttribute]
        public string Name = null;

        [XmlAttribute]
        public string Symbol = null;

        [XmlAttribute]
        public string UISymbol = null;

        [XmlAttribute]
        public string Unit = null;

        [XmlArray("Units")]
        [XmlElement("Unit", typeof(BaseUnitNode))]
        public BaseUnitNode[] Units;
    }
}