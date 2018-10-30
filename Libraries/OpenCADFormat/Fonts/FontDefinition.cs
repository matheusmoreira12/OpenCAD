using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.Drawing;

namespace OpenCAD.OpenCADFormat.Fonts.DocumentStandard
{
    [Serializable]
    public class FontGlyph
    {
        [XmlElement]
        public Canvas Canvas;

        [XmlAttribute]
        public char Unicode;
    }

    public class FontVariant
    {
    }

    [Serializable]
    public class FontDefinition
    {
        [XmlElement]
        public List<FontGlyph> Glyphs;

        [XmlAttribute]
        public string FontName;

        [XmlElement]
        public Metadata Metadata;
    }
}