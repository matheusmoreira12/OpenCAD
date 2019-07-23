using System.Collections.Generic;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class PCBLayout
    {
        [XmlAttribute]
        public Point Origin = Point.Zero;

        [XmlElement(typeof(AnnotationLayer))]
        [XmlElement(typeof(KeepoutLayer))]
        [XmlElement(typeof(MechanicalLayer))]
        [XmlElement(typeof(OverlayLayer))]
        [XmlElement(typeof(RoutingLayer))]
        [XmlElement(typeof(SolderMaskLayer))]
        [XmlElement(typeof(SolderPasteLayer))]
        public List<Layer> Layers;

        [XmlElement(typeof(Pad))]
        [XmlElement(typeof(Via))]
        public List<PCBElement> Elements;

        public PCBLayout()
        {
            Layers = new List<Layer> {
                new AnnotationLayer(){ Name = "Fabrication Notes" },
                new MechanicalLayer(){ Name = "Mechanical" },
                new KeepoutLayer(){ Name = "Keepout" },
                new OverlayLayer(){ Name = "Top Overlay" },
                new SolderMaskLayer(){ Name = "Top Solder Mask" },
                new SolderPasteLayer(){ Name = "Top Solder Paste" },
                new RoutingLayer(){ Name = "Top Copper" },
                new RoutingLayer(){ Name = "Bottom Copper" },
                new SolderPasteLayer(){ Name = "Bottom Solder Paste" },
                new SolderMaskLayer(){ Name = "Bottom Solder Mask" },
                new OverlayLayer(){ Name = "Bottom Overlay" },
            };

            Elements = new List<PCBElement>();
        }
    }
}
