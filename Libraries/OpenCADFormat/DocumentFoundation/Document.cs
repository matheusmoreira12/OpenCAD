using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.DocumentFoundation
{
    public abstract class Document
    {
        protected internal Document(DateTime created, DateTime modified, Version sourceVersion, Metadata metadata)
        {
            Created = created;
            Modified = modified;
            SourceVersion = sourceVersion ?? throw new ArgumentNullException(nameof(sourceVersion));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public DateTime Created;

        public DateTime Modified;

        public Version SourceVersion;

        public Metadata Metadata;
    }
}
