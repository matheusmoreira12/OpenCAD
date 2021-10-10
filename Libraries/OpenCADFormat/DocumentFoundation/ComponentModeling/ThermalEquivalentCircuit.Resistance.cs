using System;
using System.Xml.Serialization;

namespace OpenEDA.OpenEDAFormat.ComponentModeling
{
    public partial class ThermalEquivalentCircuit
    {
        [Serializable]
        public class Resistance
        {
            [XmlAttribute]
            public string Symbol;

            [XmlAttribute("ConnectedNets")]
            public string ATTR_ConnectedNets
            {
                get => Utils.JoinConnectedNetsAttribute(ConnectedNets);
                set => ConnectedNets = Utils.SplitConenctedNetsAttribute(value);
            }

            [XmlIgnore]
            public string[] ConnectedNets { get; private set; }
        }
    }
}
