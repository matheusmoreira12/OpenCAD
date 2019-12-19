using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
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