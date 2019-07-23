using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using OpenCAD.OpenCADFormat.CoordinateSystem;
using OpenCAD.OpenCADFormat.Drawing;
using OpenCAD.OpenCADFormat.MetaInformation;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public partial class Component
    {
        [XmlAttribute]
        public string ReferenceDesignator = "*?";

        [XmlAttribute]
        public string Name = "*?";

        [XmlAttribute]
        public string Value = "*";

        [XmlArray()]
        [XmlArrayItem("Field")]
        public MetadataFieldCollection Metadata;

        [XmlArray]
        [XmlArrayItem("Part")]
        public List<ComponentPart> Parts;

        [XmlElement]
        public Footprint Footprint = null;

        public Component()
        {
            Metadata = new MetadataFieldCollection {
                new MetadataField{ Name = "Manufacturer", Value = "", IsRequired = true },
                new MetadataField{ Name = "Datasheet", Value = "", IsRequired = true },
                new MetadataField{ Name = "Comment", Value = "" },
            };

            Parts = new List<ComponentPart>
            {
                new ComponentPart(),
            };
        }
    }
}