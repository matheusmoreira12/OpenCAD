using OpenEDA.OpenEDAFormat.ComponentModeling.Parameters;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements
{
    public abstract class BehavioralElement : IdealElement
    {
        protected BehavioralElement(string name) : base(name) { }

        public abstract Parameter InputParameter { get; }

        public abstract string ResponseFunction { get; }
    }
}
