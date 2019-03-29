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
        [XmlArray()]
        [XmlArrayItem("Field")]
        public MetadataFieldCollection Metadata;

        [XmlElement("Variant")]
        public List<Font> Font;
    }
}