using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Netting
{
    public abstract class Net
    {
        protected Net(string name, string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }

        public abstract bool IsGlobal { get; }

        public readonly string Name;

        public readonly string Description;
    }
}