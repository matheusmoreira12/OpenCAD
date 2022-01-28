using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class TracePoint
    {
        public TracePoint(Point point, TraceMode mode)
        {
            Point = point;
            Mode = mode;
        }

        public Point Point;

        public TraceMode Mode;
    }
}