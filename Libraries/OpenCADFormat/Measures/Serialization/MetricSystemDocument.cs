using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    public class MetricSystemDocument: DocumentFoundation.Document
    {
        [XmlArray("Units")]
        [XmlElement("BaseUnit", typeof(BaseUnitEntry))]
        [XmlElement("DerivedUnit", typeof(BaseUnitEntry))]
        public List<UnitEntry> List = new List<UnitEntry> { };
    }
}
