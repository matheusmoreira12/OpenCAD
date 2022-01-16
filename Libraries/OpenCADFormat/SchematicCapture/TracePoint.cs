using System;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    [Serializable]
    public struct TracePoint
    {
        [XmlAttribute]
        public Point Point;

        [XmlAttribute]
        public TraceMode Mode;
    }
}