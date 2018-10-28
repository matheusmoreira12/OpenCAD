using System;

namespace OpenCAD.OpenCADFormat.Drawing
{
    static class Utils
    {
        public static T ComputeRecursive<T, U>(Func<U, T> predicate, Func<U, U> recursion, U self, T @default = default(T))
        {
            while (self != null)
            {
                T result = predicate(self);

                if (result != null) return result;

                self = recursion(self);
            }

            return @default;
        }

        public static int AddRecursive<T>(Func<T, int> predicate, Func<T, T> recursion, T self)
        {
            int result = 0;

            while (self != null)
            {
                result |= predicate(self);

                self = recursion(self);
            }

            return result;
        }
    }
}