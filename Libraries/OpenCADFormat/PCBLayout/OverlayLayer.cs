using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class OverlayLayer: Layer
    {
        public OverlayLayer(string name, Metadata metadata) : base(name, metadata)
        {
        }
    }
}
