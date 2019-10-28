using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenCADFormat.Measures
{
    public class MetricSystem
    {
        public string Name { get; }

        public readonly List<Quantity> Quantities;

        public readonly List<MetricPrefix> Prefixes;
    }
}
