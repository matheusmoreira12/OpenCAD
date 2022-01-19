using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.FootprintLibraries
{
    [Serializable]
    public sealed class Footprint
    {
        public readonly string Name;

        public readonly Metadata Metadata;

        public readonly FootprintLayout Layout;

        public Footprint(string name, FootprintLayout layout, Metadata metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            Layout = layout ?? throw new ArgumentNullException(nameof(layout));
        }
    }
}
