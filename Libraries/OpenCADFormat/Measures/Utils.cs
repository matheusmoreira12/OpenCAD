using OpenCAD.OpenCADFormat.Measures.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public static class Utils
    {
        public static (double X, double Y) GetScreenDPI()
        {
            using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                return (X: graphics.DpiX, Y: graphics.DpiY);
            }
        }

        public static double GetMetricPrefixValue(MetricPrefix prefix) => prefix == null ? 1 : prefix.Multiplier;

        public static IEnumerable<Unit> GetSupportedUnits()
        {
            var allUnits = MetricSystemManager.GetAllUnits().ToArray();
            var allPrefixes = MetricSystemManager.GetAllMetricPrefixes();

            foreach (var unit in allUnits)
            {
                yield return unit;

                if (unit is BaseUnit)
                {
                    foreach (var prefix in allPrefixes)
                        yield return unit.Multiply(prefix);
                }
            }
        }

        internal static Quantity FindEquivalentQuantity(Unit unit)
        {
            ///TODO: implement equivalency between quantities and selection logic

            return null;
        }
    }
}