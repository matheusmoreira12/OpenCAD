using OpenCAD.OpenCADFormat.ComponentLibraries;
using System;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public class HierarchicalSheet : Sheet
    {
        public HierarchicalSheet()
        {
            Symbol = null;
        }

        public HierarchicalSheet(ComponentSymbol symbol, HierarchicalSheet[] hierarchicalSheets): base(hierarchicalSheets)
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        }

        public readonly ComponentSymbol Symbol;
    }
}