using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public sealed class ComponentPart
    {
        public ComponentPart(string name, ComponentSymbol symbol, Metadata metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public readonly string Name;

        public readonly ComponentSymbol Symbol;

        public readonly Metadata Metadata;
    }
}