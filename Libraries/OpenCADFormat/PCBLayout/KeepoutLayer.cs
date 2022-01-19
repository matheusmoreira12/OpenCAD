using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class KeepoutLayer : Layer
    {
        public KeepoutLayer(string name, Metadata metadata) : base(name, metadata)
        {
        }
    }
}
