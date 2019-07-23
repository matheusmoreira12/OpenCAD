using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class LayerStackup: SortedList<int, Layer>, IXmlSerializable
    {
        [XmlIgnore]
        public List<Layer> Layers;

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
