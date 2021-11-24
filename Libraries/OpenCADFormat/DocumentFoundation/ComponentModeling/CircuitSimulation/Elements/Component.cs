using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.CircuitElements.Components
{
    public sealed class Component : Element
    {
        public Component(string name, Terminal[] terminals, SubCircuit subCircuit) : base(name)
        {
            Terminals = terminals ?? throw new ArgumentNullException(nameof(terminals));
            SubCircuit = subCircuit ?? throw new ArgumentNullException(nameof(subCircuit));
        }

        public readonly SubCircuit SubCircuit;

        public override Terminal[] Terminals { get; }
    }
}
