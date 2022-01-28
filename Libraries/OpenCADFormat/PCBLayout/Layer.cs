using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public abstract class Layer
    {
        internal protected Layer(string name, Metadata metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public readonly string Name;

        public readonly Metadata Metadata;
    }
}
