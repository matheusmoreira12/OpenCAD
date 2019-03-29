using System;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.MetaInformation;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public class Footprint
    {
        [XmlArray]
        [XmlArrayItem("Field")]
        public MetadataFieldCollection Metadata;

        [XmlElement]
        public PCBLayout.PCBLayout Layout;

        public Footprint()
        {
            Metadata = new MetadataFieldCollection {
                new MetadataField{ Name = "Manufacturer", Value = "", IsRequired = true },
                new MetadataField{ Name = "Datasheet", Value = "", IsRequired = true },
                new MetadataField{ Name = "Comment", Value = "" },
            };

            Layout = new PCBLayout.PCBLayout();
        }
    }
}
