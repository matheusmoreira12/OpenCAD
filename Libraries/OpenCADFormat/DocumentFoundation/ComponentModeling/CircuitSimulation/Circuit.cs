using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements;
using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Netting;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling
{
    public abstract class Circuit
    {
        protected Circuit(Element[] elements, Net[] nets, NetTerminalConnection[] netTerminalConnections)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            Nets = nets ?? throw new ArgumentNullException(nameof(nets));
            NetTerminalConnections = netTerminalConnections ?? throw new ArgumentNullException(nameof(netTerminalConnections));
        }

        public readonly Element[] Elements;

        public readonly Net[] Nets;

        public readonly NetTerminalConnection[] NetTerminalConnections;
    }
}