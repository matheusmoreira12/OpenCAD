using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class Path : Shape
    {
        public List<PathSegment> PathSegments { get; private set; }
    }
}