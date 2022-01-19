using OpenCAD.OpenCADFormat.Libraries;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public class Via : PCBElement
    {
        public Via(Reference<Layer> layer) : base(layer)
        {
        }
    }
}
