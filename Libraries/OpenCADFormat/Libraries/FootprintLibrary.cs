using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    [XmlRoot]
    public class FootprintLibrary: Library
    {
        [XmlArray]
        [XmlArrayItem("Footprint")]
        public List<Footprint> Footprints;

        public FootprintLibrary()
        {
            Footprints = new List<Footprint>();
        }
    }
}
