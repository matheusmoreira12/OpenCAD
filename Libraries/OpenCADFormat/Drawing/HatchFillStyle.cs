using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class HatchFillStyle : FillStyle
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