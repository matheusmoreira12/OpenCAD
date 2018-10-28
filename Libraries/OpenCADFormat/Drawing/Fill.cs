using OpenCAD.OpenCADFormat.Measures;
using System;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum FillStyle { None, Solid, Hatched }

    public class HatchStyle
    {
        public static readonly HatchStyle Default = new HatchStyle(new[] { StrokeStyle.Solid });

        public HatchStyle(StrokeStyle[] strokeStyles)
        {
            StrokeStyles = strokeStyles ?? throw new ArgumentNullException(nameof(strokeStyles));
        }

        public StrokeStyle[] StrokeStyles { get; private set; }
    }

    public class HatchAttributes
    {
        public static readonly HatchAttributes Default = new HatchAttributes(HatchStyle.Default, 
            StrokeAttributes.Default.Thickness);

        public HatchAttributes(HatchStyle style, Measurement strokeThickness)
        {
            Style = style ?? throw new ArgumentNullException(nameof(style));
            Thickness = strokeThickness ?? throw new ArgumentNullException(nameof(strokeThickness));
        }

        public HatchStyle Style { get; private set; }
        public Measurement Thickness { get; private set; }
    }

    public struct FillAttributes
    {
        public static readonly FillAttributes Default = new FillAttributes(FillStyle.Solid, HatchAttributes.Default);

        public FillAttributes(FillStyle style, HatchAttributes hatch)
        {
            Style = style;
            Hatch = hatch;
        }

        public FillStyle Style { get; private set; }
        public HatchAttributes Hatch { get; private set; }
    }
}