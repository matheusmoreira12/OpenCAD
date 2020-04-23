using System.Collections.Generic;

namespace OpenCAD.APIs.Measures.Conversion
{
    public class Recursion<T>: HashSet<T>
    {
        public Recursion() { }

        public Recursion(IEnumerable<T> items) : base(items) { }

        public Recursion<T> Recurse(T item)
        {
            var newRecursion = new Recursion<T>(this);
            newRecursion.Add(item);
            return newRecursion;
        }
    }
}
