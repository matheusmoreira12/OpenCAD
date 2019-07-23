using System.Collections.Generic;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class Polygon : Shape
    {
        public List<Point> Points;

        protected Polygon()
        {
            Points = new List<Point>();
        }
    }
}