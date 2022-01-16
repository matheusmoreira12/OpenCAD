using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public abstract class PCBElement
    {
        [XmlElement]
        public string LayerName;
    }
}
