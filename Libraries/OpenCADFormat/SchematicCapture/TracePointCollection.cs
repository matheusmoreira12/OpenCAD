using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public class TracePointCollection : Collection<TracePoint>
    {
        public TracePointCollection()
        {
        }

        public TracePointCollection(IList<TracePoint> list) : base(list)
        {
        }
    }
}