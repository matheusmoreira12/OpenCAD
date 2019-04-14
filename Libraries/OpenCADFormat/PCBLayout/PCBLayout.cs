using System.Collections.Generic;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class PCBLayout
    {
        [XmlAttribute]
        public Point Origin = Point.Zero;

        [XmlElement(typeof(MechanicalLayer))]
        [XmlElement(typeof(KeepoutLayer))]
        [XmlElement(typeof(RoutingLayer))]
        [XmlElement(typeof(OverlayLayer))]
        [XmlElement(typeof(SolderMaskLayer))]
        [XmlElement(typeof(SolderPasteLayer))]
        public List<Layer> Layers;

        [XmlElement(typeof(Pad))]
        [XmlElement(typeof(Via))]
        public List<PCBElement> Elements;

        public PCBLayout()
        {
            Layers = new List<Layer> {
                new AnnotationLayer(){ Name = "FabricationNotesLayer", StackupOrder = 0 },
                new MechanicalLayer(){ Name = "MechanicalLayer", StackupOrder = 0 },
                new KeepoutLayer(){ Name = "KeepoutLayer", StackupOrder = 0 },
                new OverlayLayer(){ Name = "TopOverlayLayer", StackupOrder = 4 },
                new SolderMaskLayer(){ Name = "TopSolderMaskLayer", StackupOrder = 3 },
                new SolderPasteLayer(){ Name = "TopSolderPasteLayer", StackupOrder = 2 },
                new RoutingLayer(){ Name = "TopLayer", StackupOrder = 1 },
                new RoutingLayer(){ Name = "BottomLayer", StackupOrder = -1 },
                new SolderPasteLayer(){ Name = "BottomSolderPasteLayer", StackupOrder = -2 },
                new SolderMaskLayer(){ Name = "BottomSolderMaskLayer", StackupOrder = -3 },
                new OverlayLayer(){ Name = "BottomOverlayLayer", StackupOrder = -4 },
            };

            Elements = new List<PCBElement> ();
        }
    }
}
