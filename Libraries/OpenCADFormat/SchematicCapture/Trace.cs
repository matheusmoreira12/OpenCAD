using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public enum TraceMode { Auto }

    public class TraceRendererControl: System.Windows.Control
    {

    }

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