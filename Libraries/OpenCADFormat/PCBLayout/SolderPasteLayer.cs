using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class SolderPasteLayer: Layer
    {            
        public SolderPasteLayer(string name, Metadata metadata) : base(name, metadata)
        {
        }
    }
}
