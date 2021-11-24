using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Netting;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.Netting
{
    public class SubCircuitNet : Net
    {
        public SubCircuitNet(SubCircuit subCircuit, string name, string description = null) : base(name, description)
        {
            SubCircuit = subCircuit ?? throw new ArgumentNullException(nameof(subCircuit));
        }

        public readonly SubCircuit SubCircuit;

        public override bool IsGlobal => false;
    }
}