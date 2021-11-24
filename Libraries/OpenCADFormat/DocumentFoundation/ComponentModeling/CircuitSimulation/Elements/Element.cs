using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements
{
    public abstract class Element
    {
        protected Element(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public readonly string Name;

        public abstract Terminal[] Terminals { get; }
    }
}
