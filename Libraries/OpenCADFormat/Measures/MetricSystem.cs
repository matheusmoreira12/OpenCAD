using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class MetricSystem
    {
        public MetricSystem(string name, IList<Quantity> quantities, IList<MetricPrefix> prefixes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantities = (quantities ?? throw new ArgumentNullException(nameof(quantities))).ToArray();
            Prefixes = (prefixes ?? throw new ArgumentNullException(nameof(prefixes))).ToArray();
        }

        public string Name { get; }

        public Quantity[] Quantities { get; }

        public MetricPrefix[] Prefixes { get; }
    }
}
