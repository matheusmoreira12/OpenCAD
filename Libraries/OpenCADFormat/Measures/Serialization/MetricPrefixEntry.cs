using OpenCAD.OpenCADFormat.Serialization;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public sealed class MetricPrefixEntry: IResolvable<MetricPrefix>
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Symbol;

        [XmlAttribute]
        public string UISymbol;

        public void Assimilate(MetricPrefix value)
        {
            throw new System.NotImplementedException();
        }

        public MetricPrefix Resolve()
        {
            throw new System.NotImplementedException();
        }
    }
}