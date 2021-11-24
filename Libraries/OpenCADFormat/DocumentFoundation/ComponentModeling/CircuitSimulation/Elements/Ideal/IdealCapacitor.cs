namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements
{
    public sealed class IdealCapacitor : IdealElement
    {
        public IdealCapacitor(string name, double value) : base(name)
        {
            Value = value;
        }

        public readonly double Value;

        public override Terminal[] Terminals => new[] {
            new Terminal(this, 0, "A"),
            new Terminal(this, 1, "B"),
        };
    }
}
