using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    [Serializable]
    public abstract class Layer
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public int StackupOrder;

        [XmlAttribute]
        public Color UIColor;
    }
}
