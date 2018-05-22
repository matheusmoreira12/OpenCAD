using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenCAD.UserInterface
{
    static class Utils
    {
        public static (double sr, double sg, double sb, double s, double br) GetSaturatedRGB(double r, double g, double b)
        {
            double max = r;

            if (g > max)
                max = g;

            if (b > max)
                max = b;

            double min = r;

            if (g < min)
                min = g;

            if (b < min)
                min = b;

            double s = max - min;

            if (s == 0)
                r = g = b = 1;
            else
            {
                r = (r - min) / s;
                g = (g - min) / s;
                b = (b - min) / s;
            }

            double br = max;

            return (r, g, b, s, br);
        }

        public static (double r, double g, double b) GetDesaturatedRGB(double sr, double sg, double sb, double s, double br)
        {
            if (s <= 1)
            {
                sr += (1 - sr) * (1 - s);
                sg += (1 - sg) * (1 - s);
                sb += (1 - sb) * (1 - s);
            }

            sr *= br;
            sg *= br;
            sb *= br;

            return (sr, sg, sb);
        }

        public static (double s, double br) ToSwatchCoordinates(double width, double height, double cursorX, double cursorY)
        {
            double s = cursorX / width;
            double br = 1 - cursorY / height;

            return (s, br);
        }

        public static (double cursorX, double cursorY) FromSwatchCoordinates(double width, double height, double s, double br)
        {
            double cursorX = s * width;
            double cursorY = (1 - br) * height;

            return (cursorX, cursorY);
        }

        public static (double r, double g, double b) GetSwatchColorAt(double sr, double sg, double sb, double width, double height, double x, double y)
        {
            double s, br;
            (s, br) = ToSwatchCoordinates(width, height, x, y);

            return GetDesaturatedRGB(sr, sg, sb, s, br);
        }

        public static BitmapSource RenderSwatch(double width, double height, double sr, double sg, double sb, double a)
        {
            const int dpi = 96;

            WriteableBitmap swatchBitmap = new WriteableBitmap((int)width, (int)height, dpi, dpi, PixelFormats.Bgra32, null);

            int pixelBufferStride = swatchBitmap.BackBufferStride;
            int bytesPerBufferPixel = swatchBitmap.Format.BitsPerPixel / 8;

            byte[] pixelBuffer = new byte[pixelBufferStride * swatchBitmap.PixelHeight];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    double r, g, b;
                    (r, g, b) = GetSwatchColorAt(sr, sg, sb, width, height, x, y);

                    int i = y * pixelBufferStride + x * bytesPerBufferPixel;

                    pixelBuffer[i] = (byte)(b * 255);
                    pixelBuffer[i + 1] = (byte)(g * 255);
                    pixelBuffer[i + 2] = (byte)(r * 255);
                    pixelBuffer[i + 3] = (byte)(a * 255);
                }

            swatchBitmap.WritePixels(new Int32Rect(0, 0, swatchBitmap.PixelWidth, swatchBitmap.PixelHeight), pixelBuffer, pixelBufferStride, 0);

            return swatchBitmap;
        }
    }

    /// <summary>
    /// A color swatch that allows the user to finely pick custom color shades.
    /// </summary>
    public class ColorPickerSwatch : Control
    {
        static ColorPickerSwatch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerSwatch), new FrameworkPropertyMetadata(typeof(ColorPickerSwatch)));
        }

        /// <summary>
        /// Gets or sets the initial color before customization.
        /// </summary>
        public ColorInfo Color
        {
            get { return (ColorInfo)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(ColorInfo), typeof(ColorPickerSwatch));

        private void renderSwatch(DrawingContext drawingContext, double width, double height)
        {
            ImageBrush swatchBrush = new ImageBrush(SwatchBitmap);
            swatchBrush.Stretch = Stretch.Fill;

            drawingContext.DrawRectangle(swatchBrush, new Pen(), new Rect(0, 0, width, height));
        }

        private void renderCursor(DrawingContext drawingContext, double width, double height)
        {
            //Gather cursor position
            double cx, cy;
            (cx, cy) = Utils.FromSwatchCoordinates(width, height, CursorCoordinates.s, CursorCoordinates.br);

            drawingContext.DrawEllipse(Brushes.Transparent, new Pen(Brushes.Black, 1), new Point(cx, cy), 6, 6);
            drawingContext.DrawEllipse(Brushes.Transparent, new Pen(Brushes.White, 1), new Point(cx, cy), 5, 5);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double width = ActualWidth;
            double height = ActualHeight;

            drawingContext.DrawRectangle(Background, new Pen(), new Rect(0, 0, width, height));

            drawingContext.PushClip(new RectangleGeometry(new Rect(0, 0, width, height)));

            renderSwatch(drawingContext, width, height);
            renderCursor(drawingContext, width, height);
        }

        protected void WriteSelectedColor()
        {
            //Gather color information
            double sr, sg, sb, s, br;
            (sr, sg, sb, s, br) = Utils.GetSaturatedRGB(Color.R, Color.G, Color.B);

            //Gather swatch cursor position
            (s, br) = CursorCoordinates;

            //De-saturate color
            double r, g, b;
            (r, g, b) = Utils.GetDesaturatedRGB(sr, sg, sb, s, br);

            Color = new ColorInfo(r, g, b, Color.A);
        }

        protected BitmapSource SwatchBitmap = null;

        private void updateSwatch()
        {
            //Gather color information
            double sr, sg, sb, s, br;
            (sr, sg, sb, s, br) = Utils.GetSaturatedRGB(Color.R, Color.G, Color.B);

            //Update cursor coordinates
            CursorCoordinates = (s, br);

            //Generate updated 255x255 swatch bitmap
            BitmapSource swatchBitmap = Utils.RenderSwatch(255, 255, sr, sg, sb, Color.A);
            SwatchBitmap = swatchBitmap;
        }

        protected void UpdateColor()
        {
            updateSwatch();

            InvalidateVisual();
        }

        protected bool IsUserDragging = false;
        protected (double s, double br) CursorCoordinates;

        protected void StartUserDragging(Point pointerPosition)
        {
            UpdateCursorCoordinates(pointerPosition);

            IsUserDragging = true;
        }

        protected void EndUserDragging()
        {
            if (IsUserDragging)
                WriteSelectedColor();

            IsUserDragging = false;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.ChangedButton == MouseButton.Left)
                StartUserDragging(e.GetPosition(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

                EndUserDragging();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

                EndUserDragging();
        }

        protected void UpdateCursorCoordinates(Point pointerPosition)
        {
            CursorCoordinates = Utils.ToSwatchCoordinates(ActualWidth, ActualHeight, pointerPosition.X, pointerPosition.Y);

            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsUserDragging)
                UpdateCursorCoordinates(e.GetPosition(this));
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == ColorProperty)
                UpdateColor();
        }

        public ColorPickerSwatch()
        {
            Color = new ColorInfo();
        }
    }
}
