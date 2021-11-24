using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.CircuitElements.Components;
using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements;
using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Netting;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling
{
    public class SubCircuit : Circuit
    {
        public readonly Component Component;

        public SubCircuit(Component component, NetTerminalConnection[] netTerminalConnections, Element[] elements, Net[] nets) : base(netTerminalConnections, elements, nets)
        {
            Component = component ?? throw new ArgumentNullException(nameof(component));
        }
    }
}
