using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.OpenCADFormat.DataStrings;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    [Function("point")]
    public struct Point
    {
        [StringLiteral]
        public IMeasurement<Measures.Quantities.Length> X;
        [StringLiteral]
        public IMeasurement<Measures.Quantities.Length> Y;
    }

    [Function("size")]
    public struct Size
    {
        [StringLiteral]
        public IMeasurement<Measures.Quantities.Length> Width;
        [StringLiteral]
        public IMeasurement<Measures.Quantities.Length> Height;
    }

    [Function("transform")]
    public class Transform
    {
        [StringLiteral]
        public Point Translation;
        [StringLiteral]
        public IMeasurement<Measures.Quantities.PlaneAngle> Rotation;
    }
}
