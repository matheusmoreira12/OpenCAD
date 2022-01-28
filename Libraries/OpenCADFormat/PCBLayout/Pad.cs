using OpenCAD.OpenCADFormat.Libraries;
using System;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class Pad: PCBElement
    {
        public Pad(PadShape shape, Reference<Layer> layer): base(layer)
        {
            Shape = shape ?? throw new ArgumentNullException(nameof(shape));
        }

        public PadShape Shape;
    }
}
