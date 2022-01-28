using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.MetaAnnotation
{
    public sealed class Metadata
    {
        public readonly MetadataFieldCollection Fields;

        public Metadata()
        {
            Fields = new MetadataFieldCollection();
        }

        public Metadata(params MetadataField[] fields)
        {
            Fields = new MetadataFieldCollection(fields ?? throw new ArgumentNullException(nameof(fields)));
        }

        public Metadata(IList<MetadataField> fields)
        {
            Fields = new MetadataFieldCollection(fields ?? throw new ArgumentNullException(nameof(fields)));
        }
    }
}
