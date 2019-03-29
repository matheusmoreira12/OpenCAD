using System;
using System.Xml;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.MetaInformation
{
    [Serializable]
    public class MetadataField
    {
        [XmlAttribute]
        public string Name = null;

        [XmlText]
        public string Value = null;

        [XmlAttribute]
        public bool IsRequired = false;
        [XmlAttribute]
        public bool IsReadonly = false;

        [XmlIgnore]
        public bool IsValid => String.IsNullOrEmpty(Name) && Value != null;

        public bool ShouldSerialize => !IsValid;
    }
}