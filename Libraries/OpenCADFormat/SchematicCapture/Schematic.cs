using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class Schematic
    {
        public Schematic(IList<SchematicSheet> sheets)
        {
            Sheets = new SchematicSheetCollection(sheets ?? throw new ArgumentNullException(nameof(sheets)));
        }

        public readonly SchematicSheetCollection Sheets;
    }
}
