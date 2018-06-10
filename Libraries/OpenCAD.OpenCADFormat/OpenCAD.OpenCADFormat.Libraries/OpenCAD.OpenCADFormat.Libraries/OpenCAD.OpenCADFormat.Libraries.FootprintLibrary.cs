using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    [XmlRoot(ElementName = "FootprintLibrary")]
    public class FootprintLibrary: Library
    {

        [Serializable]
        public class Footprint
        {
            [XmlArray("Parameters")]
            [XmlArrayItem(ElementName = "Parameter")]
            public ParameterList Parameters;
        }

        [XmlArray("Footprints")]
        [XmlArrayItem(ElementName = "Footprint")]
        public List<Footprint> Footprints;

        public FootprintLibrary()
        {
            Footprints = new List<Footprint>();
        }
    }
}
