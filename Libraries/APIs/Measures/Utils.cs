using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    static class Utils
    {
        private const int STACK_OVERFLOW_CHECK_THRESHOLD = 64;

        public static bool VerifyStackOverflow()
        {
            var trace = new StackTrace(1, false);
            var traceFrames = trace.GetFrames();
            if (traceFrames.Length < STACK_OVERFLOW_CHECK_THRESHOLD)
                return false;
            else
            {
                var traceMethods = traceFrames.Select(f => f.GetMethod());
                var lastMethod = traceFrames.Last().GetMethod();
                int lastMethodReocurrenceCount = traceMethods.Count(m => m.Equals(lastMethod));
                return lastMethodReocurrenceCount >= STACK_OVERFLOW_CHECK_THRESHOLD;
            }
        }

        public static bool NullableEquals(object a, object b)
        {
            return a?.Equals(b) ?? a == b;
        }

        public static TSource GetLowestOrDefault<TSource, TKey>(this IEnumerable<TSource> enumerable, 
            Func<TSource, TKey> keySelector)
            => enumerable.OrderBy(keySelector).FirstOrDefault();

        public static TSource GetHighestOrDefault<TSource, TKey>(this IEnumerable<TSource> enumerable,
            Func<TSource, TKey> keySelector)
            => enumerable.OrderByDescending(keySelector).FirstOrDefault();
    }
}
