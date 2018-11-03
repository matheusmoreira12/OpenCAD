using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.Drawing;
using OpenCAD.OpenCADFormat.MetaInformation;

namespace OpenCAD.OpenCADFormat.Fonts.DocumentStandard
{
    [Serializable]
    public class Document
    {
        [XmlElement("Metadata")]
        public Metadata Metadata;

        [XmlElement("Variant")]
        public List<Font> Font;
    }
}