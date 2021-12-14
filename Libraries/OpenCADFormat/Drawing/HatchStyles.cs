namespace OpenCAD.OpenCADFormat.Drawing
{
    public static class HatchStyles
    {
        public static readonly HatchStyle Solid = new HatchStyle(new[] { StrokeStyles.Solid });
        public static readonly HatchStyle Dashed = new HatchStyle(new[] { StrokeStyles.Dashed });
        public static readonly HatchStyle Dotted = new HatchStyle(new[] { StrokeStyles.Dotted });
    }
}