
using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class PathArc : PathSegment
    {
        public static PathArc Create(Size radius, Measurement rotation, bool largeArcFlag, bool sweepFlag)
        {
            Validation.Expect(Quantities.PlaneAngle, rotation);

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
        public Measurement Rotation { get; private set; }
        public bool LargeArcFlag { get; private set; }
        public bool SweepFlag { get; private set; }
    }
}