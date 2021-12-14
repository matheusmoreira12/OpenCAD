using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextFontElement : TextFormatElement
    {
        public TextFontElement() { }

        public TextFontElement(IList<object> children) : base(children) { }

        [XmlAttribute]
        public string Family = null;

        [XmlAttribute]
        public Scalar? Height = null;
    }
}