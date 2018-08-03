using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    [XmlRoot]
    public class FootprintLibrary: Library
    {

        [Serializable]
        public class Footprint
        {
            [XmlArray]
            [XmlArrayItem(ElementName = "Parameter")]
            public ParameterList Parameters;

            /*[XmlElement]
            public BoardLayout Layout;*/
        }

        [XmlArray]
        [XmlArrayItem(ElementName = "Footprint")]
        public List<Footprint> Footprints;

        public FootprintLibrary()
        {
            Footprints = new List<Footprint>();
        }
    }
}
