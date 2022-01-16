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

        public PCB(Point origin, LayerStackup layerStackup, IList<PCBElement> elements, Metadata metadata) : base(metadata)
        {
            Origin = origin;
            LayerStackup = layerStackup ?? throw new ArgumentNullException(nameof(layerStackup));
            Elements = new List<PCBElement>(elements ?? throw new ArgumentNullException(nameof(elements)));
        }

        public PCB()
        {
            Origin = Point.Zero;
            LayerStackup = new LayerStackup(
                new AnnotationLayer("Fabrication Notes"),
                new MechanicalLayer("Mechanical"),
                new KeepoutLayer("Keepout"),
                new OverlayLayer("Top Overlay"),
                new SolderMaskLayer("Top Solder Mask"),
                new SolderPasteLayer("Top Solder Paste"),
                new RoutingLayer("Top Copper"),
                new RoutingLayer("Bottom Copper"),
                new SolderPasteLayer("Bottom Solder Paste"),
                new SolderMaskLayer("Bottom Solder Mask"),
                new OverlayLayer("Bottom Overlay"));
            Elements = new List<PCBElement>();
        }
    }
}
