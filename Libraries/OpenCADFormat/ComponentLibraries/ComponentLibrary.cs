using OpenCAD.OpenCADFormat.DocumentFoundation;
using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public sealed class ComponentLibrary : Document
    {
        public ComponentLibrary(
            DateTime created,
            DateTime modified,
            Version sourceVersion,
            Metadata metadata,
            IList<Component> components) : base(created, modified, sourceVersion, metadata)
        {
            Components = new ComponentCollection(components ?? throw new ArgumentNullException(nameof(components)));
        }

        public readonly ComponentCollection Components;
    }
}