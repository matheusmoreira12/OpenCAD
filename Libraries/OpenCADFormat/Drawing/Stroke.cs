using OpenCAD.APIs.Measures;
using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public static class StrokeStyles
    {
        public static readonly StrokeStyle Solid = new StrokeStyle(new[] { 1 });
        public static readonly StrokeStyle Dashed = new StrokeStyle(new[] { 3, 3 });
        public static readonly StrokeStyle Dotted = new StrokeStyle(new[] { 1, 1 });
        public static readonly StrokeStyle DashDot = new StrokeStyle(new[] { 3, 1, 1, 1 });
        public static readonly StrokeStyle DashDotDot = new StrokeStyle(new[] { 3, 1, 1, 1, 1, 1 });
        public static readonly StrokeStyle DotDashDash = new StrokeStyle(new[] { 1, 1, 3, 3, 3, 1 });
    }

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

    public struct StrokeAttributes
    {
        public static readonly StrokeAttributes Default = new StrokeAttributes(StrokeStyles.Solid, Scalar.Parse("10mil"));

        public StrokeAttributes(StrokeStyle style, Scalar thickness)
        {
            Style = style;
            Thickness = thickness;
        }

        public StrokeStyle Style { get; private set; }
        public Scalar Thickness { get; private set; }
    }
}