using OpenCAD.OpenCADFormat.DocumentFoundation;
using OpenCAD.OpenCADFormat.MetaAnnotation;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public sealed class ComponentLibrary : Document
    {
        public ComponentLibrary()
        {
            Components = new List<Component>();
        }

        public ComponentLibrary(Metadata metadata) : base(metadata)
        {
        }

        public readonly List<Component> Components;
    }
}