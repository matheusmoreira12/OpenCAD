using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Image : DrawingNode
    {
        public string EmbeddedResourceID { get; private set; }
        public Point TopLeft { get; private set; }
        public Size Size { get; private set; }
    }
}