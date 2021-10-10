using System;
using System.Xml.Serialization;

namespace OpenEDA.OpenEDAFormat.ComponentModeling
{
    [Serializable]
    public partial class ThermalEquivalentCircuit
    {
        [XmlElement("Net", Type = typeof(Net))]
        [XmlElement("HeatSource", Type = typeof(HeatSource))]
        [XmlElement("TemperatureSource", Type = typeof(TemperatureSource))]
        [XmlElement("Resistance", Type = typeof(Resistance))]
        public object[] Elements;
    }
}
