using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    static class Utils
    {
        public static bool VerifyStackOverflow()
        {
            var trace = new StackTrace();
            var methods = trace.GetFrames().Select(f => f.GetMethod()).ToArray();
            if (methods.Length < 2)
                return false;
            else
            {
                var lastMethod = methods[methods.Length - 2];
                int ocurrenceCount = methods.Count(m => m == lastMethod);
                bool isStackOverflowing = ocurrenceCount > 1;
                return isStackOverflowing;
            }
        }

        public static bool NullableEquals(object a, object b)
        {
            return a?.Equals(b) ?? a == b;
        }

        public static TSource GetLowest<TSource, TKey>(this IEnumerable<TSource> enumerable, 
            Func<TSource, TKey> keySelector)
            => enumerable.OrderBy(keySelector).First();

        public static TSource GetHighest<TSource, TKey>(this IEnumerable<TSource> enumerable,
            Func<TSource, TKey> keySelector)
            => enumerable.OrderByDescending(keySelector).First();
    }
}
