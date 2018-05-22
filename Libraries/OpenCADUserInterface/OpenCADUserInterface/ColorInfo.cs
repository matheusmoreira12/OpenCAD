using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OpenCAD.UserInterface
{
    public class ColorInfo : DependencyObject
    {
        public static ColorInfo FromColor(Color color)
        {
            return new ColorInfo(color.R, color.G, color.B, color.A);
        }

        public Color ToColor(ColorInfo info)
        {
            return Color.FromArgb((byte)(A * 255), (byte)(R * 255), (byte)(G * 255), (byte)(B * 255));
        }

        public double R
        {
            get { return (double)GetValue(RProperty); }
            set { SetValue(RProperty, value); }
        }

        // Using a DependencyProperty as the backing store for R.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RProperty =
            DependencyProperty.Register("R", typeof(double), typeof(ColorInfo), new PropertyMetadata(0.0));

        public double G
        {
            get { return (double)GetValue(GProperty); }
            set { SetValue(GProperty, value); }
        }

        // Using a DependencyProperty as the backing store for R.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GProperty =
            DependencyProperty.Register("G", typeof(double), typeof(ColorInfo), new PropertyMetadata(0.0));

        public double B
        {
            get { return (double)GetValue(BProperty); }
            set { SetValue(BProperty, value); }
        }

        // Using a DependencyProperty as the backing store for R.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BProperty =
            DependencyProperty.Register("B", typeof(double), typeof(ColorInfo), new PropertyMetadata(0.0));

        public double A
        {
            get { return (double)GetValue(AProperty); }
            set { SetValue(AProperty, value); }
        }

        // Using a DependencyProperty as the backing store for R.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AProperty =
            DependencyProperty.Register("A", typeof(double), typeof(ColorInfo), new PropertyMetadata(0.0));

        public ColorInfo() { }
        public ColorInfo(double r, double g, double b, double a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
