using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public abstract class Sheet
    {
        protected internal Sheet(IList<HierarchicalSheet> hierarchicalSheets)
        {
            HierarchicalSheets = new HierarchicalSheetCollection(hierarchicalSheets ?? throw new ArgumentNullException(nameof(hierarchicalSheets)));
        }

        public readonly HierarchicalSheetCollection HierarchicalSheets;
    }
}