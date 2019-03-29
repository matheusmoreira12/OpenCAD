using System;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.MetaInformation;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public class ComponentPart
    {
        [XmlAttribute]
        public string Name = "*";

        [XmlArray()]
        [XmlArrayItem("Field")]
        public MetadataFieldCollection Metadata;

        [XmlElement]
        public SchematicCapture.SchematicCapture Symbol;

        public ComponentPart()
        {
            Metadata = new MetadataFieldCollection {
                new MetadataField{ Name = "Comment", Value = "" },
            };

            Symbol = new SchematicCapture.SchematicCapture();
        }
    }
}