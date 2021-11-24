using System;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    public class DerivedQuantityDimension
    {
        public static DerivedQuantityDimension Parse(string value)
        {
            throw new NotImplementedException();
        }

        public DerivedQuantityDimension(params DerivedQuantityDimensionMember[] members)
        {
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        public DerivedQuantityDimensionMember[] Members { get; }
    }
}