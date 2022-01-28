using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class SchematicSheetCollection : Collection<SchematicSheet>
    {
        public SchematicSheetCollection()
        {
        }

        public SchematicSheetCollection(IList<SchematicSheet> list) : base(list)
        {
        }
    }
}