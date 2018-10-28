using OpenCAD.OpenCADFormat.Measures;
using System;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum StrokeSegment { Dash, Dot }

    public class StrokeStyle
    {
        public static readonly StrokeStyle None = new StrokeStyle(false);
        public static readonly StrokeStyle Solid = new StrokeStyle(true);
        public static readonly StrokeStyle Dashed = new StrokeStyle(new[] { StrokeSegment.Dash });
        public static readonly StrokeStyle Dotted = new StrokeStyle(new[] { StrokeSegment.Dot });
        public static readonly StrokeStyle DashDot = new StrokeStyle(new[] { StrokeSegment.Dash, StrokeSegment.Dot });
        public static readonly StrokeStyle DashDotDot = new StrokeStyle(new[] { StrokeSegment.Dash, StrokeSegment.Dot,
            StrokeSegment.Dot });
        public static readonly StrokeStyle DotDashDash = new StrokeStyle(new[] { StrokeSegment.Dot, StrokeSegment.Dash,
            StrokeSegment.Dash });

        public StrokeStyle(bool isSolid)
        {
            Segments = new StrokeSegment[] { };
            IsSolid = isSolid;
        }

        public StrokeStyle(StrokeSegment[] segments)
        {
            Segments = segments ?? throw new ArgumentNullException(nameof(segments));
            IsSolid = false;
        }

        public StrokeSegment[] Segments { get; private set; }
        public bool IsSolid { get; private set; }
    }

    public struct StrokeAttributes
    {
        public static readonly StrokeAttributes Default = new StrokeAttributes(StrokeStyle.None, new Measurement(10,
            Units.Length.Mil));

        public StrokeAttributes(StrokeStyle style, Measurement thickness)
        {
            Style = style;
            Thickness = thickness ?? throw new ArgumentNullException(nameof(thickness));
        }

        public StrokeStyle Style { get; private set; }
        public Measurement Thickness { get; private set; }
    }
}