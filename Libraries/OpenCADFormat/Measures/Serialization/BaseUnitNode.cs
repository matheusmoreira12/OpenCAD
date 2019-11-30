using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class BaseUnitNode : UnitNode
    {
        public string Quantity { get; internal set; }
    }
}