using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Netting
{
    public class NetTerminalConnection
    {
        public readonly Net Net;

        public readonly Terminal Terminal;

        public NetTerminalConnection(Net net, Terminal terminal)
        {
            Net = net ?? throw new ArgumentNullException(nameof(net));
            Terminal = terminal ?? throw new ArgumentNullException(nameof(terminal));
        }
    }
}
