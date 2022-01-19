using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class AnnotationLayer : Layer
    {
        public AnnotationLayer(string name, Metadata metadata): base(name, metadata)
        {
        }
    }
}
