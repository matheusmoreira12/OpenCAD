using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public abstract class PCBElement
    {
        [XmlElement]
        public string LayerName;
    }
}
