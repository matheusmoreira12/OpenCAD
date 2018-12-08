using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public enum TraceMode { Auto, Left90deg, Right90deg, Straight }

    public struct TracePoint
    {
        public Point Point;
        public TraceMode Mode;
    }

    public class Trace
    {
        public TracePoint[] points;
    }
}