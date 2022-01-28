using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class StrokeStyle
    {
        public StrokeStyle(int[] dashArray)
        {
            DashArray = makeDashArrayLengthEven(dashArray ?? throw new ArgumentNullException(nameof(dashArray)));

            int[] makeDashArrayLengthEven(int[] dashArray)
            {
                if (dashArray.Length % 2 != 0)
                    return dashArray.Concat(new int[] { 0 }).ToArray();
                return dashArray;
            }
        }

        public readonly int[] DashArray;

        public readonly bool IsSolid;
    }
}