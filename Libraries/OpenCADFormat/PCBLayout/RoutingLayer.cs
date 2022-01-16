using System;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.MetaAnnotation;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class RoutingLayer: Layer
    {
        public RoutingLayer(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CopperPullBack = DefaultValues.COPPER_PULLBACK;
            ThermalRelief = new ThermalReliefOptions();
            CopperTheeving = new CopperThievingOptions();
            Metadata = new Metadata(new MetadataField("Notes", ""));
        }

        public RoutingLayer(
            string name, Scalar copperPullback, ThermalReliefOptions thermalRelief, CopperThievingOptions copperTheeving,
            Metadata metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CopperPullBack = copperPullback;
            ThermalRelief = thermalRelief ?? throw new ArgumentNullException(nameof(thermalRelief));
            CopperTheeving = copperTheeving ?? throw new ArgumentNullException(nameof(thermalRelief));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public override string Name { get; }

        public override Metadata Metadata { get; }

        public readonly Scalar CopperPullBack;

        public readonly ThermalReliefOptions ThermalRelief;

        public readonly CopperThievingOptions CopperTheeving;
    }
}
