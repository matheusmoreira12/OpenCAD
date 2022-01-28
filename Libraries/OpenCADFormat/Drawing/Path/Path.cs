using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Path : Shape
    {
        public Path(
            List<PathSegment> pathSegments,
            StrokeAttributes stroke,
            FillStyle fill) : base(
                stroke,
                fill)
        {
            PathSegments = pathSegments ?? throw new ArgumentNullException(nameof(pathSegments));
        }

        public readonly List<PathSegment> PathSegments;
    }
}