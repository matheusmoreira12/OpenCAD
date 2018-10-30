using OpenCAD.OpenCADFormat.Measures;
using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class HatchStyle
    {
        public static readonly HatchStyle Solid = new HatchStyle(new[] { StrokeStyle.Solid });
        public static readonly HatchStyle Dashed = new HatchStyle(new[] { StrokeStyle.Dashed });
        public static readonly HatchStyle Dotted = new HatchStyle(new[] { StrokeStyle.Dotted });
        public static readonly HatchStyle SolidDashed = new HatchStyle(new[] { StrokeStyle.Solid, StrokeStyle.Dashed });
        public static readonly HatchStyle SolidDotted = new HatchStyle(new[] { StrokeStyle.Solid, StrokeStyle.Dotted });

        public HatchStyle(StrokeStyle[] strokeStyles)
        {
            StrokeStyles = strokeStyles ?? throw new ArgumentNullException(nameof(strokeStyles));
        }

        public StrokeStyle[] StrokeStyles { get; private set; }
    }

    public class HatchAttributes
    {
        public HatchAttributes(HatchStyle style, Measurement thickness, Measurement angle, double density)
        {
            Style = style;
            Thickness = thickness;
            Angle = angle;
            Density = density;
        }

        public HatchStyle Style { get; private set; }
        public Measurement Thickness { get; private set; }
        public Measurement Angle { get; private set; }
        public double Density { get; private set; }
    }

    public abstract class FillStyle
    {
        public static readonly FillStyle None = null;
        public static readonly FillStyle Solid = new SolidFillStyle();
        public static readonly FillStyle Hatched = new HatchFillStyle(new[] { new HatchAttributes(HatchStyle.Solid, 
            StrokeAttributes.Default.Thickness, new Measurement(0, Units.PlaneAngle.Degree), .1) });
        public static readonly FillStyle Hatched90Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyle.Solid,
            StrokeAttributes.Default.Thickness, new Measurement(90, Units.PlaneAngle.Degree), .1) });
        public static readonly FillStyle Hatched45Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyle.Solid,
            StrokeAttributes.Default.Thickness, new Measurement(45, Units.PlaneAngle.Degree), .1) });
        public static readonly FillStyle Hatched135Deg = new HatchFillStyle(new[] { new HatchAttributes(HatchStyle.Solid,
            StrokeAttributes.Default.Thickness, new Measurement(135, Units.PlaneAngle.Degree), .1) });
        public static readonly FillStyle Crossed = HatchFillStyle.Combine((HatchFillStyle)Hatched, 
            (HatchFillStyle)Hatched90Deg);
        public static readonly FillStyle Crossed45Deg = HatchFillStyle.Combine((HatchFillStyle)Hatched45Deg,
            (HatchFillStyle)Hatched135Deg);
    }

    public class SolidFillStyle: FillStyle
    {
    }

    public class HatchFillStyle: FillStyle
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