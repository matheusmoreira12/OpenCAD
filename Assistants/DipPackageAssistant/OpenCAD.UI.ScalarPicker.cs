using OpenCAD.OpenCADFormat.Measures;
using System.Windows;
using System.Windows.Controls;

namespace OpenCAD.UI
{
    public class ScalarPicker : Control
    {
        static ScalarPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScalarPicker), new FrameworkPropertyMetadata(typeof(ScalarPicker)));
        }

        public Unit SelectedUnit
        {
            get { return (Unit)GetValue(SelectedUnitProperty); }
            set { SetValue(SelectedUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedUnitProperty =
            DependencyProperty.Register("SelectedUnit", typeof(Unit), typeof(ScalarPicker));

        public double SelectedAmount
        {
            get { return (double)GetValue(SelectedAmountProperty); }
            set { SetValue(SelectedAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedAmount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedAmountProperty =
            DependencyProperty.Register("SelectedAmount", typeof(double), typeof(ScalarPicker), new PropertyMetadata(0.0));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_TxtEdit = GetTemplateChild("PART_TxtEdit") as TextBox;
            PART_TxtUnit = GetTemplateChild("PART_TxtUnit") as TextBlock;

            PART_TxtEdit.TextChanged += PART_TxtEdit_TextChanged;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == SelectedAmountProperty)
            {
                if (PART_TxtUnit is null) return;

                PART_TxtEdit.Text = SelectedAmount.ToString();
            }
            else if (e.Property == SelectedUnitProperty)
            {
                if (PART_TxtEdit is null) return;

                PART_TxtUnit.Text = SelectedUnit?.UISymbol;
            }
        }

        protected TextBox PART_TxtEdit = null;
        protected TextBlock PART_TxtUnit = null;



        private void PART_TxtEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = PART_TxtEdit.Text;
            Scalar scalar;

            if (Scalar.TryParse(input, out scalar))
            {
                SelectedAmount = scalar.Amount;
                SelectedUnit = scalar.Unit;
            }
        }
    }
}
