using OpenCAD.OpenCADFormat.CoordinateSystem;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class PathArc : PathSegment
    {
        public static PathArc Create(Size radius, Scalar rotation, bool largeArcFlag, bool sweepFlag)
        {
            return new PathArc
            {
                Radius = radius,
                Rotation = rotation,
                LargeArcFlag = largeArcFlag,
                SweepFlag = sweepFlag
            };
        }

        private PathArc() { }

        public Size Radius { get; private set; }
        public Scalar Rotation { get; private set; }
        public bool LargeArcFlag { get; private set; }
        public bool SweepFlag { get; private set; }
    }
}