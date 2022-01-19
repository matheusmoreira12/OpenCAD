using OpenCAD.OpenCADFormat.PCBLayout;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.FootprintLibraries
{
    public sealed class FootprintLayout
    {
        public readonly List<PCBElement> Elements;

        public readonly LayerStackup LayerStackup;

        public FootprintLayout(IList<PCBElement> elements, LayerStackup layerStackup)
        {
            Elements = new List<PCBElement>(elements ?? throw new ArgumentNullException(nameof(elements)));
            LayerStackup = layerStackup ?? throw new ArgumentNullException(nameof(layerStackup));
        }
    }
}