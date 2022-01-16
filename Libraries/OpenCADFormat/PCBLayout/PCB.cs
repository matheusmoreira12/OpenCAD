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
            LayerStackup = new LayerStackup {
                /*{ 1, new Layer("Fabrication Notes", LayerType.Annotation) },
                { 2, new Layer("Mechanical", LayerType.Mechanical) },
                { 3, new Layer("Keepout", LayerType.Keepout) },
                { 4, new Layer("Top Overlay", LayerType.Overlay) },
                { 5, new Layer("Top Solder Mask", LayerType.SolderMask) },
                { 6, new Layer("Top Solder Paste", LayerType.SolderPaste) },
                { 7, new Layer("Top Copper", LayerType.Routing) },
                { 8, new Layer("Bottom Copper", LayerType.Routing) },
                { 9, new Layer("Bottom Solder Paste", LayerType.SolderPaste) },
                { 10, new Layer("Bottom Solder Mask", LayerType.SolderMask) },
                { 11, new Layer("Bottom Overlay", LayerType.Overlay) },*/
            };
            Elements = new List<PCBElement>();
        }
    }
}
