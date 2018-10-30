using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class Canvas
    {
        [XmlArray()]
        [XmlElement("Element")]
        public List<DrawingElement> Elements;
    }
}
