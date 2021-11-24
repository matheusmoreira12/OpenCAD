using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements
{
    public sealed class Terminal
    {
        public readonly Element Element;

        public Terminal(Element element, int order, string name)
        {
            Element = element ?? throw new ArgumentNullException(nameof(element));
            Order = order;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public readonly int Order;

        public readonly string Name;
    }
}