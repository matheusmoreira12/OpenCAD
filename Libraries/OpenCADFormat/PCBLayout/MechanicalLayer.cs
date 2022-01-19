using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class MechanicalLayer: Layer
    {
        public MechanicalLayer(string name, Metadata metadata) : base(name, metadata)
        {
        }
    }
}
