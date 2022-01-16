using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class OverlayLayer: Layer
    {
        public OverlayLayer(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Metadata = new Metadata(new MetadataField("Notes", ""));
        }

        public OverlayLayer(string name, Metadata metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public override string Name { get; }

        public override Metadata Metadata { get; }
    }
}
