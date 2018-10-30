using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.Drawing;

namespace OpenCAD.OpenCADFormat.Fonts.DocumentStandard
{
    [Serializable]
    public class Document
    {
        [XmlArray("Metadata")]
        [XmlElement("Field")]
        public MetadataField Metadata;

        [XmlElement("Variant")]
        public List<Font> Font;
    }

    [Serializable]
    public struct MetadataField
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public string Value;

        public MetadataField(string name, string value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}