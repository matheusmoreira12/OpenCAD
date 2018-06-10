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

    public class DataGridColorColumn : DataGridBoundColumn
    {
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            ColorPicker picker = new ColorPicker();
            picker.SetBinding(ColorPicker.ColorProperty, Binding);
            //picker.DataContext = dataItem;

            return picker;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            ColorIndicator indicator = new ColorIndicator();
            indicator.SetBinding(ColorIndicator.IndicatorColorProperty, Binding);
            //indicator.DataContext = dataItem;

            return indicator;
        }
    }
}
