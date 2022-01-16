using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.DocumentFoundation
{
    public abstract class Document
    {
        public Metadata Metadata;

        protected Document(Metadata metadata)
        {
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public Document()
        {
            Metadata = new Metadata(
                new MetadataField("Title", "Untitled", isRequired: true),
                new MetadataField("Description", "", isRequired: true),
                new MetadataField("Author", "", isRequired: true),
                new MetadataField("Creation Date", DateTime.Now.ToLongDateString(), isRequired: true),
                new MetadataField("Modification Date", DateTime.Now.ToLongDateString(), isRequired: true),
                new MetadataField("Notes", "", isRequired: true)
            );
        }
    }
}
