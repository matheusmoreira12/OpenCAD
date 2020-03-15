using System;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    public class DerivedUnitDimension: IEquatable<DerivedUnitDimension>
    {
        public DerivedUnitDimension(params DerivedUnitDimensionMember[] members)
        {
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        public DerivedUnitDimensionMember[] Members { get; }

        bool IEquatable<DerivedUnitDimension>.Equals(DerivedUnitDimension other)
        {
            if (Utils.VerifyStackOverflow())
                return true;
            else
            {
                var this_OrderedMembers = Members.OrderBy(m => m.BaseUnit.Name).ThenBy(m => m.BaseUnit.MetricSystem)
                    .ThenBy(m => m.Prefix).ThenBy(m => m.Exponent);
                var other_OrderedMembers = Members.OrderBy(m => m.BaseUnit.Name).ThenBy(m => m.BaseUnit.MetricSystem)
                    .ThenBy(m => m.Prefix).ThenBy(m => m.Exponent);
                return Enumerable.SequenceEqual(this_OrderedMembers, other_OrderedMembers,
                    new IEquatableEqualityComparer<DerivedUnitDimensionMember>());
            }
        }
    }
}
