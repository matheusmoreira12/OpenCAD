using System;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    public class DerivedQuantityDimension : IEquatable<DerivedQuantityDimension>
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

        bool IEquatable<DerivedQuantityDimension>.Equals(DerivedQuantityDimension other)
        {
            if (Utils.VerifyStackOverflow())
                return true;
            else
            {
                var this_OrderedMembers = Members.OrderBy(m => m.BaseQuantity.Name)
                    .ThenBy(m => m.BaseQuantity.MetricSystem).ThenBy(m => m.Exponent);
                var other_OrderedMembers = Members.OrderBy(m => m.Exponent).OrderBy(m => m.BaseQuantity.Name)
                    .ThenBy(m => m.BaseQuantity.MetricSystem).ThenBy(m => m.Exponent);
                return Enumerable.SequenceEqual(this_OrderedMembers, other_OrderedMembers,
                    new IEquatableEqualityComparer<DerivedQuantityDimensionMember>());
            }
        }
    }
}