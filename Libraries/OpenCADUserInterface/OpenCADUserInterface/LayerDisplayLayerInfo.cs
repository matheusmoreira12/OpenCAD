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
    public class LayerDisplayLayerInfo : DependencyObject
    {
        public Color LayerColor
        {
            get { return (Color)GetValue(LayerColorProperty); }
            set { SetValue(LayerColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LayerColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayerColorProperty =
            DependencyProperty.Register("LayerColor", typeof(Color), typeof(LayerDisplayLayerInfo));

        public string LayerLabel
        {
            get { return (string)GetValue(LayerLabelProperty); }
            set { SetValue(LayerLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LayerLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayerLabelProperty =
            DependencyProperty.Register("LayerLabel", typeof(string), typeof(LayerDisplayLayerInfo));

        public bool LayerVisible
        {
            get { return (bool)GetValue(LayerVisibleProperty); }
            set { SetValue(LayerVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LayerVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayerVisibleProperty =
            DependencyProperty.Register("LayerVisible", typeof(bool), typeof(LayerDisplayLayerInfo));
    }
}
