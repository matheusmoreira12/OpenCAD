using System;
using System.Collections.Generic;

using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.SchematicCapture.Drawing
{
    public enum FontWeight { Lighter, Light, Normal, Bold, Bolder }

    public class Font
    {
        public string EmbeddedFontID;
        public FontWeight Weight;
        public Measurement<Measures.Quantities.Length> Height;
    }

    public abstract class Shape
    {
    }

    public class Arc : Shape
    {
        public Point Center;
        public Measurement<Measures.Quantities.Length> Radius;
        public Measurement<Measures.Quantities.PlaneAngle> StartAngle;
        public Measurement<Measures.Quantities.PlaneAngle> SweepAngle;

        public Arc(Point center, Measurement<Measures.Quantities.Length> radius, 
            Measurement<Measures.Quantities.PlaneAngle> startAngle, 
            Measurement<Measures.Quantities.PlaneAngle> sweepAngle)
        {
            Center = center;
            Radius = radius;
            StartAngle = startAngle ?? throw new ArgumentNullException("startAngle");
            SweepAngle = sweepAngle ?? throw new ArgumentNullException("sweepAngle");
        }
    }

    public class ArcThreePoint : Shape
    {
        public ArcThreePoint(Point start, Point end, Point control)
        {
            Start = start;
            End = end;
            Control = control;
        }

        public ToArc()
        {
            var unit = Measures.Quantities.Length.Meter;
            var length = Start.Distance(End, unit);
        }

        public Point Start;
        public Point End;
        public Point Control;
    }

    public class ArcCentered : Shape
    {
        public Arc ToArc()
        {
            var unit = Measures.Quantities.Length.Meter;

            var radius0 = Start.Distance(Center, unit);
            var radius1 = End.Distance(Center, unit);
            var radius = radius0 > radius1 ? radius0 : radius1;

            var startAngle = Start.Angle(Center);
            var sweepAngle = Point.Angle(End, Center, Start);

            return new Arc(Center, radius, startAngle, sweepAngle);
        }

        public Point Start;
        public Point End;
        public Point Center;
    }

    public class Circle : Shape
    {
        public Point Center;
        public Measurement<Measures.Quantities.Length> Radius;
    }

    public class Ellipse : Shape
    {
        public Point Center;
        public Size Radius;
    }

    public class Image : Shape
    {
        public string EmbeddedImageID;
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

    public class Text : Shape
    {
        public string Content;
        public Font Font;
    }

    public abstract class PathSegment
    {
        public Point EndPoint;
        public bool Relative;
    }

    public class PathArc : PathSegment
    {
        public Size Radius;
        public Measurement<Measures.Quantities.PlaneAngle> Rotation;
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

    public class Path : Shape
    {
        public List<PathSegment> PathSegments;
    }
}