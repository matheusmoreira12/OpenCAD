using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class MetricSystem
    {
        public MetricSystem(string name, IList<Quantity> quantities, IList<Unit> units, 
            IList<MetricPrefix> prefixes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantities = (quantities ?? throw new ArgumentNullException(nameof(quantities))).ToList();
            Units = (units ?? throw new ArgumentNullException(nameof(units))).ToList();
            Prefixes = (prefixes ?? throw new ArgumentNullException(nameof(prefixes))).ToList();
        }

        public string Name { get; }

        public string FullName { get; }

        public List<Quantity> Quantities { get; }

        public List<Unit> Units { get; }

        public List<MetricPrefix> Prefixes { get; }
    }
}
