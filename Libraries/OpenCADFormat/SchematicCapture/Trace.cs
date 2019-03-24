using System;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public enum TraceMode { Auto, Left90deg, Right90deg, Straight }

    [Serializable]
    public struct TracePoint
    {
        [XmlAttribute]
        public Point Point;

        [XmlAttribute]
        public TraceMode Mode;
    }

    public class Trace
    {
        [XmlElement(typeof(TracePoint))]
        public TracePoint[] points;
    }
}