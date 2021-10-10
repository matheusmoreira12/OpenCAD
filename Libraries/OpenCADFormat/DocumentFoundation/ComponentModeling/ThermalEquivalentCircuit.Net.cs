using System;
using System.Xml.Serialization;

namespace OpenEDA.OpenEDAFormat.ComponentModeling
{
    public partial class ThermalEquivalentCircuit
    {
        [Serializable]
        public class Net
        {
            [XmlAttribute]
            public string Symbol;
        }
    }
}
