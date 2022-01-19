using System;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.MetaAnnotation;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class RoutingLayer: Layer
    {
        public RoutingLayer(
            string name,
            Scalar copperPullback,
            ThermalReliefOptions thermalRelief,
            CopperBalancingOptions copperTheeving,
            Metadata metadata) : base(name, metadata)
        {
            CopperPullBack = copperPullback;
            ThermalRelief = thermalRelief ?? throw new ArgumentNullException(nameof(thermalRelief));
            CopperTheeving = copperTheeving ?? throw new ArgumentNullException(nameof(thermalRelief));
        }

        public readonly Scalar CopperPullBack;

        public readonly ThermalReliefOptions ThermalRelief;

        public readonly CopperBalancingOptions CopperTheeving;
    }
}
