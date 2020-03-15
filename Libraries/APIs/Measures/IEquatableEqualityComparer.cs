using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures
{
    internal class IEquatableEqualityComparer<T> : EqualityComparer<T>
    {
        public override bool Equals(T x, T y) => (x as IEquatable<T>).Equals(y);

        public override int GetHashCode(T obj) => obj.GetHashCode();
    }
}
