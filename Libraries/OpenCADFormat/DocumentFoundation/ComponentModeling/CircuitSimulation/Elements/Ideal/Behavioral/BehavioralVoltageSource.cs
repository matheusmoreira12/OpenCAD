using OpenEDA.OpenEDAFormat.ComponentModeling.Parameters;
using System;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Elements.Behavioral
{
    public class BehavioralVoltageSource : BehavioralElement
    {
        public BehavioralVoltageSource(Parameter inputParameter, string responseFunction, string name) : base(name)
        {
            InputParameter = inputParameter ?? throw new ArgumentNullException(nameof(inputParameter));
            ResponseFunction = responseFunction ?? throw new ArgumentNullException(nameof(responseFunction));
        }

        public override Terminal[] Terminals => new[] {
            new Terminal(this, 0, "A"),
            new Terminal(this, 1, "B"),
        };

        public override Parameter InputParameter { get; }

        public override string ResponseFunction { get; }
    }
}
