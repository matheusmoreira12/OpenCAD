using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    /// <summary>
    /// Represents a draw1ing element's stroke style.
    /// </summary>
    public class StrokeStyle
    {
        private int[] fixDashArray(int[] dashArray)
        {
            if (dashArray.Length % 2 != 0)
                //Add a zero to the dash-array.
                return dashArray.Concat(new int[] { 0 }).ToArray();

            return dashArray;
        }

        public StrokeStyle(int[] dashArray)
        {
            DashArray = fixDashArray(dashArray ?? throw new ArgumentNullException(nameof(dashArray)));
        }

        public int[] DashArray { get; private set; }
        public bool IsSolid { get; private set; }
    }
}