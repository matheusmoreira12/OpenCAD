using System;
using System.Collections.Generic;

using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public abstract class DrawingElement
    {
        public StrokeAttributes Stroke = StrokeAttributes.Default;
        public FillStyle Fill = FillStyle.None;
    }

    [Serializable]
    public abstract class Shape : DrawingElement
    {

    }

    public enum ArcType { Centered, ThreePoint, CenteredStartSweepAngle }

    public class Arc : Shape
    {
        public static Arc CreateCentered(Point center, Point start, Point end)
        {
            return new Arc
            {
                Type = ArcType.Centered,
                Center = center,
                Start = start,
                End = end
            };
        }

        public static Arc CreateThreePoint(Point start, Point end, Point control)
        {
            return new Arc
            {
                Type = ArcType.ThreePoint,
                Start = start,
                End = end,
                Control = control
            };
        }

        public static Arc CreateCenteredStartSweepAngle(Point center, Size radius, Measurement rotation,
            Measurement startAngle, Measurement sweepAngle)
        {
            Validation.Expect(Quantities.PlaneAngle, rotation);

            return new Arc
            {
                Type = ArcType.CenteredStartSweepAngle,
                Center = center,
                Radius = radius,
                Rotation = rotation,
                StartAngle = startAngle,
                SweepAngle = sweepAngle
            };
        }

        private Arc() { }

        public ArcType Type { get; private set; }
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point Control { get; private set; }
        public Point Center { get; private set; }
        public Size Radius { get; private set; }
        public Measurement Rotation { get; private set; }
        public Measurement StartAngle { get; private set; }
        public Measurement SweepAngle { get; private set; }
    }

    public enum EllipseType { Centered, TwoPoint, ThreePoint }

    public class Ellipse : Shape
    {
        public static Ellipse CreateCentered(Point center, Size radius,
            Measurement rotation)
        {
            return new Ellipse
            {
                Type = EllipseType.Centered,
                Center = center,
                Radius = radius,
                Rotation = rotation
            };
        }

        public static Ellipse CreateTwoPoint(Point start, Point end)
        {
            return new Ellipse
            {
                Type = EllipseType.TwoPoint,
                Start = start,
                End = end
            };
        }

        public static Ellipse CreateThreePoint(Point start, Point end, Point control)
        {
            return new Ellipse
            {
                Type = EllipseType.ThreePoint,
                Start = start,
                End = end,
                Control = control
            };
        }

        private Ellipse() { }

        public EllipseType Type { get; private set; }
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point Control { get; private set; }
        public Point Center { get; private set; }
        public Size Radius { get; private set; }
        public Measurement Rotation { get; private set; }
    }

    public class Image : Shape
    {
        public string EmbeddedResourceID;
        public Point TopLeft;
        public Size Size;
    }

    public class Line : Shape
    {
        public Point Start;
        public Point End;
    }

    public class Polyline : Shape
    {
        public List<Point> Points;
    }

    public class Polygon : Shape
    {
        public List<Point> Points;
    }

    public class Path : Shape
    {
        public List<PathSegment> PathSegments;
    }

    public abstract class PathSegment
    {
        public Point EndPoint;
        public bool Relative;
    }

    public class PathArc : PathSegment
    {
        public Size Radius;
        public Measurement Rotation;
        public bool LargeArcFlag;
        public bool SweepFlag;
    }

    public class PathCurveCubic : PathSegment
    {
        public List<Point> ControlPoints;
    }

    public class PathCurveQuadratic : PathSegment
    {
        public List<Point> ControlPoints;
    }

    public class PathLine : PathSegment
    {
    }

    public class PathPoint : PathSegment
    {
    }
}