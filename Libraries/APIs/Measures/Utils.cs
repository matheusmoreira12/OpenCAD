using System.Diagnostics;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    static class Utils
    {
        public static bool verifyStackOverflow()
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
    }
}
