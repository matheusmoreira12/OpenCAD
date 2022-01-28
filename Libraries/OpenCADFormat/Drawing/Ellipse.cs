using OpenCAD.OpenCADFormat.CoordinateSystem;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Ellipse : Shape
    {
        public Point? MajorAxisStartPoint;

        public Point? MajorAxisEndPoint;

        public Point? MinorAxisPoint;

        public Point? Center;

        public Size? Radius;

        public Scalar? CircleRadius;

        public double? Excentricity;

        public Scalar? Rotation;

        public Ellipse(
            Point? majorAxisStartPoint,
            Point? majorAxisEndPoint,
            Point? minorAxisPoint,
            Point? center,
            Size? radius,
            Scalar? circleRadius,
            double? excentricity,
            Scalar? rotation,
            StrokeAttributes stroke,
            FillStyle fill) : base(
                stroke,
                fill)
        {
            MajorAxisStartPoint = majorAxisStartPoint;
            MajorAxisEndPoint = majorAxisEndPoint;
            MinorAxisPoint = minorAxisPoint;
            Center = center;
            Radius = radius;
            CircleRadius = circleRadius;
            Excentricity = excentricity;
            Rotation = rotation;
        }
    }
}