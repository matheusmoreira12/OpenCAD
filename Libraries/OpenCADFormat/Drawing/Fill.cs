using OpenCAD.APIs.Measures;
using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public static class HatchStyles
    {
        public static readonly HatchStyle Solid = new HatchStyle(new[] { StrokeStyles.Solid });
        public static readonly HatchStyle Dashed = new HatchStyle(new[] { StrokeStyles.Dashed });
        public static readonly HatchStyle Dotted = new HatchStyle(new[] { StrokeStyles.Dotted });
    }

    public class HatchStyle
    {
        public HatchStyle(StrokeStyle[] strokeStyles)
        {
            StrokeStyles = strokeStyles ?? throw new ArgumentNullException(nameof(strokeStyles));
        }

        public StrokeStyle[] StrokeStyles { get; private set; }
    }

    public class HatchAttributes
    {
        public HatchAttributes(HatchStyle style, Scalar thickness, Scalar angle, int[] hatchArray)
        {
            Style = style;
            Thickness = thickness;
            Angle = angle;
            HatchArray = hatchArray;
        }

        public HatchStyle Style { get; private set; }
        public Scalar Thickness { get; private set; }
        public Scalar Angle { get; private set; }
        public int[] HatchArray { get; private set; }
    }

    public static class FillStyles
    {
        public static readonly FillStyle None = null;
        public static readonly FillStyle Solid = new SolidFillStyle();
        public static readonly FillStyle Hatched = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("0deg"), new[] {1, 6}) });
        public static readonly FillStyle Hatched90Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("90deg"), new[] {1, 6}) });
        public static readonly FillStyle Hatched45Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("45deg"), new[] {1, 6}) });
        public static readonly FillStyle Hatched135Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyles.Solid,
            StrokeAttributes.Default.Thickness, Scalar.Parse("135deg"), new[] {1, 6}) });
        public static readonly FillStyle Crossed = HatchFillStyle.Combine((HatchFillStyle)Hatched,
            (HatchFillStyle)Hatched90Deg);
        public static readonly FillStyle Crossed45Deg = HatchFillStyle.Combine((HatchFillStyle)Hatched45Deg,
            (HatchFillStyle)Hatched135Deg);
    }

    public abstract class FillStyle
    {
    }

    public class SolidFillStyle : FillStyle
    {
    }

    public class HatchFillStyle : FillStyle
    {
        public static HatchFillStyle Combine(params HatchFillStyle[] values)
        {
            var hatches = values.SelectMany(style => style.Hatches);

            return new HatchFillStyle(hatches.ToArray());
        }

        public HatchFillStyle(HatchAttributes[] hatches)
        {
            Hatches = hatches ?? throw new ArgumentNullException(nameof(hatches));
        }

        public HatchAttributes[] Hatches { get; private set; }
    }
}