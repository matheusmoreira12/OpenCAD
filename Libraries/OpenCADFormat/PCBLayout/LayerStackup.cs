using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class LayerStackup
    {
        public LayerStackup(params Layer[] layers)
        {
            Layers = new LayerCollection(layers ?? throw new ArgumentNullException(nameof(layers)));
        }

        public LayerStackup(IList<Layer> layers)
        {
            Layers = new LayerCollection(layers ?? throw new ArgumentNullException(nameof(layers)));
        }

        public readonly LayerCollection Layers;
    }
}
