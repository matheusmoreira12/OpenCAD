using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Rendering.OpenCADFormat.Drawing
{
    public sealed class EllipseRenderer : Renderer<OpenCAD.OpenCADFormat.Drawing.Ellipse>
    {
        public EllipseRenderer(OpenCAD.OpenCADFormat.Drawing.Ellipse target) : base(target)
        {
        }

        public override void Render(DrawingContext context, Brush fill, Pen stroke)
        {
            switch (Target.Type)
            {
                case OpenCAD.OpenCADFormat.Drawing.EllipseType.Centered:
                    RenderCentered(context, fill, stroke);
                    break;

                case OpenCAD.OpenCADFormat.Drawing.EllipseType.ThreePoint:
                    RenderThreePoint(context, fill, stroke);
                    break;

                case OpenCAD.OpenCADFormat.Drawing.EllipseType.TwoPoint:
                    RenderTwoPoint(context, fill, stroke);
                    break;
            }
        }

        private void RenderFill(DrawingContext context, Brush fill, Pen stroke)
        {
            var brushRenderer = new FillStyleRenderer(Target.Fill);
            brushRenderer.Render(context, fill, stroke);
        }

        private void RenderStroke(DrawingContext context, Brush fill, Pen stroke)
        {
            var strokeStyleRenderer = new StrokeStyleRenderer(Target.Stroke.Style);
        }

        private void RenderCentered(DrawingContext context, Brush fill, Pen stroke)
        {
            
        }

        private void RenderThreePoint(DrawingContext context, Brush fill, Pen stroke)
        {
        }

        private void RenderTwoPoint(DrawingContext context, Brush fill, Pen stroke)
        {
        }
    }
}
