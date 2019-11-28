using System;
using System.Windows;
using System.Windows.Media;

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

        private void RenderCentered(DrawingContext context, Brush fill, Pen stroke)
        {
            var center = new Point(Target.Center.X.ConvertToUnit(OpenCAD.OpenCADFormat.Measures.Units.Length.PixelX).Amount,
                Target.Center.X.ConvertToUnit(OpenCAD.OpenCADFormat.Measures.Units.Length.PixelY).Amount);

            var ellipse = new EllipseGeometry();
            ellipse.Transform = new RotateTransform();

            context.DrawGeometry(fill, stroke, ellipse);
        }

        private void RenderThreePoint(DrawingContext context, Brush fill, Pen stroke)
        {
        }

        private void RenderTwoPoint(DrawingContext context, Brush fill, Pen stroke)
        {
        }
    }
}
