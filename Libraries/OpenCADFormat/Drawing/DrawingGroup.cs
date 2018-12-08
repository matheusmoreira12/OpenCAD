using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class DrawingGroup
    {
        [XmlArray("Elements")]
        [XmlElement("Arc", typeof(Arc))]
        [XmlElement("Ellipse", typeof(Ellipse))]
        [XmlElement("Image", typeof(Image))]
        [XmlElement("Line", typeof(Line))]
        [XmlElement("Polyline", typeof(Polyline))]
        [XmlElement("Polygon", typeof(Polygon))]
        [XmlElement("Path", typeof(Path))]
        public List<DrawingElement> Elements;
    }
}
