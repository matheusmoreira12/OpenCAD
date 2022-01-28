using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    /// <summary>
    /// Represents a drawing arc
    /// </summary>
    public abstract class Arc : Shape
    {
        internal protected Arc(
            Point? centerPoint,
            Point? startPoint,
            Point? controlPoint,
            Point? endPoint,
            Scalar? sweepAngle,
            Scalar? startAngle,
            Scalar? endAngle,
            Scalar? radius,
            Scalar? diameter,
            Scalar? cordLength,
            StrokeAttributes stroke,
            FillStyle fill): base(
                stroke,
                fill)
        {
            CenterPoint = centerPoint;
            StartPoint = startPoint;
            ControlPoint = controlPoint;
            EndPoint = endPoint;
            SweepAngle = sweepAngle;
            StartAngle = startAngle;
            EndAngle = endAngle;
            Radius = radius;
            Diameter = diameter;
            CordLength = cordLength;
        }

        public readonly Point? CenterPoint;

        public readonly Point? StartPoint;

        public readonly Point? ControlPoint;

        public readonly Point? EndPoint;

        public readonly Scalar? SweepAngle;

        public readonly Scalar? StartAngle;

        public readonly Scalar? EndAngle;

        public readonly Scalar? Radius;

        public readonly Scalar? Diameter;

        public readonly Scalar? CordLength;
    }
}