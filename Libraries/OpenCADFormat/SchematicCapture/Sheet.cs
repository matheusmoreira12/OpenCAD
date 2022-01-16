using System;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public class Sheet
    {
        public Sheet()
        {
            HierarchicalSheets = new HierarchicalSheet[0];
        }

        public Sheet(HierarchicalSheet[] hierarchicalSheets)
        {
            HierarchicalSheets = hierarchicalSheets ?? throw new ArgumentNullException(nameof(hierarchicalSheets));
        }

        public readonly HierarchicalSheet[] HierarchicalSheets;
    }
}