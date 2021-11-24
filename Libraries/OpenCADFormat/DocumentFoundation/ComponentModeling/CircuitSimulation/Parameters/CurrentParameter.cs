using OpenEDA.OpenEDAFormat.ComponentModeling.Probing;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.Parameters
{
    public class CurrentParameter : Parameter
    {
        public readonly Probe PointA;

        public readonly Probe PointB;

        public CurrentParameter(Probe pointA, Probe pointB)
        {
            PointA = pointA ?? throw new ArgumentNullException(nameof(pointA));
            PointB = pointB ?? throw new ArgumentNullException(nameof(pointB));
        }
    }
}
