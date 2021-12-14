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
}