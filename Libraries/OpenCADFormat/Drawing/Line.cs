using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class Line : Shape
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
    }
}