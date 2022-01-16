using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.FootprintLibraries
{
    [Serializable]
    public sealed class Footprint
    {
        public readonly string Name = "*";

        public readonly Metadata Metadata;

        public readonly FootprintLayout Layout;

        public Footprint()
        {
            Layout = new FootprintLayout();
            Metadata = new Metadata(
                new MetadataField("Manufacturer Designation", "", isRequired: true),
                new MetadataField("IEC Designation", "", isRequired: true),
                new MetadataField("DIN Designation", "", isRequired: true),
                new MetadataField("EIAJ/JEITA Designation", "", isRequired: true),
                new MetadataField("Gosstandart Designation", "", isRequired: true),
                new MetadataField("Rosstandart Designation", "", isRequired: true),
                new MetadataField("Kombinat Mikroelektronik Erfurt Designation", "", isRequired: true),
                new MetadataField("Manufacturer", "", isRequired: true),
                new MetadataField("Datasheet", "", isRequired: true),
                new MetadataField("Notes", ""));
        }

        public Footprint(string name, FootprintLayout layout, Metadata metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            Layout = layout ?? throw new ArgumentNullException(nameof(layout));
        }
    }
}
