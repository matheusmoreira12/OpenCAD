using System;
using System.Collections.Generic;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Polyline : Shape
    {
        public readonly List<Point> Points;

        public Polyline(
            List<Point> points,
            StrokeAttributes stroke,
            FillStyle fill) : base(
                stroke,
                fill)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }
    }
}