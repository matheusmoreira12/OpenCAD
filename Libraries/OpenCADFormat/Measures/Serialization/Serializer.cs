using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    class MetricSystemSerializer: XmlSerializer
    {
        public MetricSystemSerializer(): base(typeof (MetricSystem))
        {

        }
    }
}
