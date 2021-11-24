using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Netting;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.Probing
{
    public class NetProbe : Probe
    {
        public readonly Net Net;

        public NetProbe(Net net)
        {
            Net = net ?? throw new ArgumentNullException(nameof(net));
        }
    }
}
