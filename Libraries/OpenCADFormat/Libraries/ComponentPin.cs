using OpenCAD.OpenCADFormat.CoordinateSystem;
using System;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public class ComponentPin
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public ComponentPinType Type;

        [XmlAttribute]
        public ComponentPinPolarity Polarity;

        [XmlAttribute]
        public Point Placement;

        [XmlAttribute]
        public string ConnectedPadName;
    }
}
