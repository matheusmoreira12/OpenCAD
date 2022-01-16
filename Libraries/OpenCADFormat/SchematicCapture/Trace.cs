using System;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public class Trace
    {
        public Trace(TracePoint[] points)
        {
            this.points = points ?? throw new ArgumentNullException(nameof(points));
        }

        public readonly TracePoint[] points;
    }
}