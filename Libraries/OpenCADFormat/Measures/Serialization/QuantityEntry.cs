using OpenCAD.OpenCADFormat.Serialization;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public abstract class QuantityEntry: IResolvable<Quantity>
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Symbol;

        [XmlAttribute]
        public string UISymbol;

        [XmlArray("Units")]
        [XmlElement("Unit", typeof(UnitEntry))]
        public UnitEntry[] Units;

        public abstract void Assimilate(Quantity value);

        public abstract Quantity Resolve();
    }
}