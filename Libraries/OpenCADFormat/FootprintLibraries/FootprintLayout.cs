using OpenCAD.OpenCADFormat.PCBLayout;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.FootprintLibraries
{
    public sealed class FootprintLayout
    {
        public readonly List<PCBElement> Elements;

        public FootprintLayout()
        {
            Elements = new List<PCBElement>();
        }

        public FootprintLayout(IList<PCBElement> elements)
        {
            Elements = new List<PCBElement>(elements ?? throw new ArgumentNullException(nameof(elements)));
        }
    }
}