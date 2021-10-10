using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OpenEDA.OpenEDAFormat.ComponentModeling
{
    [Serializable]
    [XmlRoot("ComponentModel")]
    public class ComponentModel
    {
        [XmlAttribute]
        public string Name;

        [XmlElement("Description")]
        public XCData ATTR_Description
        {
            get => new XCData(Description);
            set => Description = value.Value;
        }

        [XmlIgnore]
        public string Description { get; private set; }

        [XmlElement()]
        public ThermalEquivalentCircuit ThermalEquivalentCircuit;
    }
}
