using System;
using System.Collections.Generic;
using OpenCAD.OpenCADFormat.CoordinateSystem;
using OpenCAD.OpenCADFormat.DocumentFoundation;
using OpenCAD.OpenCADFormat.MetaAnnotation;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class PCB : Document
    {
        public readonly Point Origin;

        public readonly LayerStackup LayerStackup;

        public readonly List<PCBElement> Elements;

        public PCB(
            Point origin,
            LayerStackup layerStackup,
            IList<PCBElement> elements,
            DateTime created,
            DateTime modified,
            Version sourceVersion,
            Metadata metadata) : base(created, modified, sourceVersion, metadata)
        {
            Origin = origin;
            LayerStackup = layerStackup ?? throw new ArgumentNullException(nameof(layerStackup));
            Elements = new List<PCBElement>(elements ?? throw new ArgumentNullException(nameof(elements)));
        }
    }
}
