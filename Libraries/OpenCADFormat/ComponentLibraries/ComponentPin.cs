using OpenCAD.OpenCADFormat.CoordinateSystem;
using OpenCAD.OpenCADFormat.Libraries;
using OpenCAD.OpenCADFormat.MetaAnnotation;
using OpenCAD.OpenCADFormat.PCBLayout;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public sealed class ComponentPin
    {
        public readonly string Name;

        public readonly ComponentPinType Type;

        public readonly ComponentPinPolarity Polarity;

        public readonly Point Placement;

        public readonly List<Reference<Pad>> ConnectedPads;

        public readonly Metadata Metadata;

        public ComponentPin()
        {
            Name = "*";
            Type = ComponentPinType.NotSpecified;
            Polarity = ComponentPinPolarity.NotSpecified;
            Metadata = new Metadata(new MetadataField("Notes", ""));
        }

        public ComponentPin(string name, ComponentPinType type, ComponentPinPolarity polarity, Point placement, IList<Reference<Pad>> connectedPads)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Polarity = polarity;
            Placement = placement;
            ConnectedPads = new List<Reference<Pad>>(connectedPads ?? throw new ArgumentNullException(nameof(connectedPads)));
        }
    }
}
