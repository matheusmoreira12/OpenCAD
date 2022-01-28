using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public abstract class PathSegment
    {
        internal protected PathSegment()
        {
        }

        public Point EndPoint { get; private set; }
        public bool Relative { get; private set; }
    }
}