using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class Trace
    {
        public Trace(IList<TracePoint> points)
        {
            Points = new TracePointCollection(points ?? throw new ArgumentNullException(nameof(points)));
        }

        public readonly TracePointCollection Points;
    }
}