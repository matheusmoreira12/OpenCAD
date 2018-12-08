using OpenCAD.OpenCADFormat.Drawing;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Fonts
{
    [Serializable]
    public class Glyph
    {
        [XmlElement]
        public DrawingGroup Canvas;

        [XmlAttribute]
        public char Unicode;
    }

}
