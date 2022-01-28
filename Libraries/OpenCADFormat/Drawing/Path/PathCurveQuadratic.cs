using System.Collections.Generic;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class PathCurveQuadratic : PathSegment
    {
        public List<Point> ControlPoints;
    }
}