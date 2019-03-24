using System.Collections.Generic;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class PathCurveCubic : PathSegment
    {
        public List<Point> ControlPoints;
    }
}