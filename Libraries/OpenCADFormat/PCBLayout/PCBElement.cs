using OpenCAD.OpenCADFormat.Libraries;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public abstract class PCBElement
    {
        protected PCBElement(Reference<Layer> layer)
        {
            Layer = layer ?? throw new ArgumentNullException(nameof(layer));
        }

        public Reference<Layer> Layer;
    }
}
