using OpenCAD.OpenCADFormat.ComponentLibraries;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class HierarchicalSheet : Sheet
    {
        public HierarchicalSheet(
            ComponentSymbol symbol,
            IList<HierarchicalSheet> hierarchicalSheets) : base(hierarchicalSheets)
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        }

        public readonly ComponentSymbol Symbol;
    }
}