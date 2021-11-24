using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements;
using OpenEDA.OpenEDAFormat.ComponentModeling.Probing;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.Netting
{
    public class TerminalProbe : Probe
    {
        public readonly Terminal Terminal;

        public TerminalProbe(Terminal terminal)
        {
            Terminal = terminal ?? throw new ArgumentNullException(nameof(terminal));
        }
    }
}
