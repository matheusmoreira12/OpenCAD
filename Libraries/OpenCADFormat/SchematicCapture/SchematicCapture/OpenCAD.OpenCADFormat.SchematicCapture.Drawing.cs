using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.SchematicCapture.Drawing
{
    public enum FontWeight { Lighter, Light, Normal, Bold, Bolder }

    public class Font
    {
        public string EmbeddedFontID;
        public FontWeight Weight;
        public IMeasurement<Measures.Quantities.Length> Height;
    }

    public abstract class Shape
    {
    }

    public class Arc : Shape
    {
        public Point Center;
        public Size Radius;
        public IMeasurement<Measures.Quantities.PlaneAngle> Start;
        public IMeasurement<Measures.Quantities.PlaneAngle> Sweep;
    }

    public class Circle : Shape
    {
        public Point Center;
        public IMeasurement<Measures.Quantities.Length> Radius;
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
        public IMeasurement<Measures.Quantities.PlaneAngle> Rotation;
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