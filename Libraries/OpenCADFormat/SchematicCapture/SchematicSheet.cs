using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class SchematicSheet : Sheet
    {
        public SchematicSheet(IList<HierarchicalSheet> hierarchicalSheets) : base(hierarchicalSheets)
        {
        }
    }
}