using OpenCAD.OpenCADFormat.MetaInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.DocumentFoundation
{
    public abstract class Document
    {
        [XmlArray()]
        [XmlArrayItem("Field")]
        public MetadataFieldCollection Metadata;

        public Document()
        {
            Metadata = new MetadataFieldCollection
            {
                new MetadataField { Name = "Title", Value = "Untitled", IsRequired = true },
                new MetadataField { Name = "Description", Value = "", IsRequired = true },
                new MetadataField { Name = "Author", Value = "", IsRequired = true },
                new MetadataField { Name = "Creation Date", Value = DateTime.Now.ToLongDateString(), IsRequired = true },
                new MetadataField { Name = "Modification Date", Value = DateTime.Now.ToLongDateString(), IsRequired = true },
                new MetadataField { Name = "Comment", Value = "", IsRequired = true },
            };
        }
    }
}
