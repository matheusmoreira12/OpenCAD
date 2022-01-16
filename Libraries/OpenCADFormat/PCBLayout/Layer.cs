using OpenCAD.OpenCADFormat.MetaAnnotation;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public abstract class Layer
    {
        public abstract string Name { get; }

        public abstract Metadata Metadata { get; }
    }
}
