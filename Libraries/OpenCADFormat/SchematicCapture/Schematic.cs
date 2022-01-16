using System;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public class Schematic
    {
        public Schematic()
        {
            Sheets = new[] { new Sheet() };
        }

        public Schematic(Sheet[] sheets)
        {
            Sheets = sheets ?? throw new ArgumentNullException(nameof(sheets));
        }

        public readonly Sheet[] Sheets;
    }
}
