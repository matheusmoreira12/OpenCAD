using OpenCAD.OpenCADFormat.FootprintLibraries;
using OpenCAD.OpenCADFormat.Libraries;
using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public sealed class Component
    {
        public Component(
            string name,
            string referenceDesignator,
            IList<ComponentPart> parts,
            Reference<Footprint> footprint,
            Metadata metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ReferenceDesignator = referenceDesignator ?? throw new ArgumentNullException(nameof(referenceDesignator));
            Parts = new ComponentPartCollection(parts ?? throw new ArgumentNullException(nameof(parts)));
            Footprint = footprint ?? throw new ArgumentNullException(nameof(footprint));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public readonly string Name;

        public readonly string ReferenceDesignator;

        public readonly ComponentPartCollection Parts;

        public readonly Reference<Footprint> Footprint;

        public readonly Metadata Metadata;
    }
}