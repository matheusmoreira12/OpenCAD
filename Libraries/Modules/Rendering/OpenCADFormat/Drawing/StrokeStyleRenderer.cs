using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Rendering.OpenCADFormat.Drawing
{
    public sealed class StrokeStyleRenderer : Renderer<OpenCAD.OpenCADFormat.Drawing.StrokeStyle>
    {
        public StrokeStyleRenderer(OpenCAD.OpenCADFormat.Drawing.StrokeStyle target) : base(target)
        {
        }

        public override void Render(DrawingContext context, Brush fill, Pen stroke)
        {
            if (Target is OpenCAD.OpenCADFormat.Drawing.HatchFillStyle)
                RenderHatchFillStyle((OpenCAD.OpenCADFormat.Drawing.HatchFillStyle)Target, context, fill, stroke);
            else if (Target is OpenCAD.OpenCADFormat.Drawing.SolidFillStyle)
                RenderSolidFillStyle((OpenCAD.OpenCADFormat.Drawing.HatchFillStyle)Target, context, fill, stroke);
        }

        private void RenderHatchFillStyle(OpenCAD.OpenCADFormat.Drawing.HatchFillStyle target, DrawingContext context, Brush fill, Pen stroke)
        {
            throw new NotImplementedException();
        }

        private void RenderSolidFillStyle(OpenCAD.OpenCADFormat.Drawing.HatchFillStyle target, DrawingContext context, Brush fill, Pen stroke)
        {
            throw new NotImplementedException();
        }
    }
}
