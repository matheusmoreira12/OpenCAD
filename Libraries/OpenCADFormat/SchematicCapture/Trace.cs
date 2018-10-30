using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public enum TraceMode { Auto }

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