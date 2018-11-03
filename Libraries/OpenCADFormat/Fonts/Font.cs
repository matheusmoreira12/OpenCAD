using System;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Fonts.DocumentStandard
{
    [Serializable]
    public class Font
    {
        [XmlAttribute]
        public string FontName;

        [XmlElement]
        public Glyph[] Glyphs;

        public Font() { }

        public Font(string fontName, Glyph[] glyphs)
        {
            FontName = fontName ?? throw new ArgumentNullException(nameof(fontName));
            Glyphs = glyphs ?? throw new ArgumentNullException(nameof(glyphs));
        }
    }
}