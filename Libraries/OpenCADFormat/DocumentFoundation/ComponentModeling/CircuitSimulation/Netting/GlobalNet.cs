using OpenEDA.OpenEDAFormat.ComponentModeling.CircuitSimulation.Netting;

namespace OpenEDA.OpenEDAFormat.ComponentModeling.Netting
{
    public class GlobalNet : Net
    {
        public GlobalNet(string name, string description = null) : base(name, description)
        {
        }

        public override bool IsGlobal => true;
    }
}
