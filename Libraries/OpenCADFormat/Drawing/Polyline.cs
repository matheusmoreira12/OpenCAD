using System.Collections.Generic;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class Polyline : Shape
    {
        public List<Point> Points { get; private set; }
    }
}