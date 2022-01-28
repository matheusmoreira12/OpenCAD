using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class HierarchicalSheetCollection : Collection<HierarchicalSheet>
    {
        public HierarchicalSheetCollection()
        {
        }

        public HierarchicalSheetCollection(IList<HierarchicalSheet> list) : base(list)
        {
        }
    }
}