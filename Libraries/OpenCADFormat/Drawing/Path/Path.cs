using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Path : Shape
    {
        public List<PathSegment> PathSegments { get; private set; }
    }
}