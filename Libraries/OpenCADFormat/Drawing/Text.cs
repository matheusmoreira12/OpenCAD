using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class Text : TextElement
    {
        public static Text FromContent(string content) => new Text(new[] { content });

        public Text() { }

        public Text(IList<object> children) : base(children) { }

        [XmlIgnore]
        protected override bool ShouldCollapse => false;

        [XmlAttribute]
        public TextAlignment? Alignment = null;

        [XmlAttribute]
        public TextAlignment? VerticalAlignment = null;
    }
}