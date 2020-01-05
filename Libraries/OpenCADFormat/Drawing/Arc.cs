using OpenCAD.OpenCADFormat.CoordinateSystem;
using System;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum ArcType { Centered, ThreePoint, CenteredStartSweep }

    [Serializable]
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

        public static Arc CreateCenteredStartSweep(Point center, Size radius, Scalar rotation,
            Scalar startAngle, Scalar sweepAngle)
        {
            return new Arc
            {
                Type = ArcType.CenteredStartSweep,
                Center = center,
                Radius = radius,
            };
        }

        private Arc() { }

        [XmlAttribute]
        [XmlEnum]
        public ArcType Type;

        [XmlAttribute]
        public Point Start;
        public bool ShouldSerializeStart => Type == ArcType.Centered || Type == ArcType.ThreePoint;

        [XmlAttribute]
        public Point End;
        public bool ShouldSerializeEnd => Type == ArcType.Centered || Type == ArcType.ThreePoint;

        [XmlAttribute]
        public Point Control;
        public bool ShouldSerializeControl => Type == ArcType.ThreePoint;

        [XmlAttribute]
        public Point Center;
        public bool ShouldSerializeCenter => Type == ArcType.Centered || 
            Type == ArcType.CenteredStartSweep;

        [XmlAttribute]
        public Size Radius;
        public bool ShouldSerializeRadius => Type == ArcType.CenteredStartSweep;
    }
}