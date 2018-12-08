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
        public static Arc CreateCentered(Point center, Point start, Point end) => new Arc
        {
            Type = ArcType.Centered,
            Center = center,
            Start = start,
            End = end
        };

        public static Arc CreateThreePoint(Point start, Point end, Point control) => new Arc
        {
            Type = ArcType.ThreePoint,
            Start = start,
            End = end,
            Control = control
        };

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
            Validation.Expect(Quantities.PlaneAngle, rotation);

            return new Ellipse
            {
                Type = EllipseType.Centered,
                Center = center,
                Radius = radius,
                Rotation = rotation
            };
        }

        public static Ellipse CreateTwoPoint(Point start, Point end) => new Ellipse
        {
            Type = EllipseType.TwoPoint,
            Start = start,
            End = end
        };

        public static Ellipse CreateThreePoint(Point start, Point end, Point control) => new Ellipse
        {
            Type = EllipseType.ThreePoint,
            Start = start,
            End = end,
            Control = control
        };

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
        public string EmbeddedResourceID { get; private set; }
        public Point TopLeft { get; private set; }
        public Size Size { get; private set; }
    }

    public class Line : Shape
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
    }

    public class Polyline : Shape
    {
        public List<Point> Points { get; private set; }
    }

    public class Polygon : Shape
    {
        public List<Point> Points { get; private set; }
    }

    public class Path : Shape
    {
        public List<PathSegment> PathSegments { get; private set; }
    }

    public abstract class PathSegment
    {
        public Point EndPoint { get; private set; }
        public bool Relative { get; private set; }
    }

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

    public enum RectangleType { TwoPoint, ThreePoint, Centered }

    public class Rectangle : Shape
    {
        public static Rectangle CreateTwoPoint(Point start, Point end) => new Rectangle()
        {
            Start = start,
            End = end
        };

        public static Rectangle CreateThreePoint(Point start, Point end, Point control) => new Rectangle()
        {
            Start = start,
            End = end,
            Control = control
        };

        public static Rectangle CreateCentered(Point center, Point end) => new Rectangle()
        {
            Center = center,
            End = end
        };

        private Rectangle() { }

        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point Control { get; private set; }
        public Point Center { get; private set; }
    }
}