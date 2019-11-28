using OpenCAD.OpenCADFormat.Serialization;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class BaseQuantityEntry : QuantityEntry
    {
        public override void Assimilate(Quantity value)
        {
            throw new System.NotImplementedException();
        }

        public override Quantity Resolve()
        {
            throw new System.NotImplementedException();
        }
    }
}