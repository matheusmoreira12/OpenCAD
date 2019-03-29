using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class PCBLayout
    {
        [XmlElement(typeof(MechanicalLayer))]
        [XmlElement(typeof(KeepoutLayer))]
        [XmlElement(typeof(RoutingLayer))]
        [XmlElement(typeof(OverlayLayer))]
        [XmlElement(typeof(SolderMaskLayer))]
        [XmlElement(typeof(SolderPasteLayer))]
        public List<Layer> Layers;

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
        }
    }
}
