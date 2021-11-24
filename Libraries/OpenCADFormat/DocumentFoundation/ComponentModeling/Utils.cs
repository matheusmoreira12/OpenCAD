using System.Linq;

namespace OpenEDA.OpenEDAFormat.ComponentModeling
{
    static class Utils
    {
        static readonly char SEPARATOR = ';';

        public static string[] SplitConenctedNetsAttribute(string attribute)
        {
            return attribute.Split(new[] { SEPARATOR }).Select(netSymbol => netSymbol.Trim()).ToArray();
        }

        public static string JoinConnectedNetsAttribute(string[] items)
        {
            return string.Join(SEPARATOR.ToString(), items.Select(netSymbol => netSymbol.Trim()));
        }
    }
}
