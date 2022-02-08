using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public abstract class PathSegment
    {
        protected internal PathSegment()
        {
        }

        public Point EndPoint { get; private set; }
        public bool Relative { get; private set; }
    }
}