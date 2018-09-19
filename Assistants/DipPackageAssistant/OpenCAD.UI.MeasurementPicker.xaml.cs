using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.UI
{
    [ValueConversion(typeof(Measurement), typeof(string))]
    public class MeasurementToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;

            Measurement input = value as Measurement;

            if (input == null)
                return DependencyProperty.UnsetValue;

            return input.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value == null ? null : value as string;

            Measurement output;

            if (input != null && Measurement.TryParse(input, out output))
                return output;

            return new ValidationResult(false, "Measurement string is invalid.");
        }
    }

    /// <summary>
    /// Interação lógica para MeasurementPicker.xam
    /// </summary>
    public partial class MeasurementPicker : UserControl
    {
        public MeasurementPicker()
        {
            InitializeComponent();
        }

        protected void EnterCodeEditingMode()
        {
            IsCodeEditingVisible = true;

            CloseDropDown();
        }

        protected void ExitCodeEditingMode() => IsCodeEditingVisible = false;

        private void measurementPicker_PreviewMouseDoubleClick(object sender,
            System.Windows.Input.MouseButtonEventArgs e) => EnterCodeEditingMode();

        protected void OpenDropDown()
        {
            IsDropDownOpen = true;

            SelectionPopup.Focus();
        }

        protected void CloseDropDown() => IsDropDownOpen = false;

        protected void UpdateSelection()
        {
            if (SelectedUnit != null)
            {
                if (SelectedUnit.IsMetric)
                    Selection = new Measurement(SelectedAmount, SelectedUnit, SelectedPrefix);
                else
                    Selection = new Measurement(SelectedAmount, SelectedUnit);
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == IsDropDownOpenProperty)
            {
                if (IsDropDownOpen)
                    IsCodeEditingVisible = true;
            }
            else if (e.Property == SelectedUnitProperty)
            {
                if (SelectedUnit.IsMetric)
                    IsMetricPrefixVisible = true;
                else
                    IsMetricPrefixVisible = false;

                UpdateSelection();
            }
            else if (e.Property == SelectedPrefixProperty)
                UpdateSelection();
            else if (e.Property == SelectedAmountProperty)
                UpdateSelection();
            else if (e.Property == SelectionProperty)
                (SelectedAmount, SelectedPrefix, SelectedUnit) = (Selection.Amount, Selection.Prefix, Selection.Unit);
        }

        #region Status Flags
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpenForSelection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(MeasurementPicker),
                new PropertyMetadata(false));

        public bool IsMetricPrefixVisible
        {
            get { return (bool)GetValue(IsMetricPrefixVisibleProperty); }
            set { SetValue(IsMetricPrefixVisibleProperty, value); }
        }
        #endregion

        #region Selection Properties
        // Using a DependencyProperty as the backing store for IsMetricPrefixVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMetricPrefixVisibleProperty =
            DependencyProperty.Register("IsMetricPrefixVisible", typeof(bool), typeof(MeasurementPicker),
                new PropertyMetadata(false));

        public bool IsCodeEditingVisible
        {
            get { return (bool)GetValue(IsCodeEditingVisibleProperty); }
            set { SetValue(IsCodeEditingVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCodeEditingActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCodeEditingVisibleProperty =
            DependencyProperty.Register("IsCodeEditingVisible", typeof(bool), typeof(MeasurementPicker),
                new PropertyMetadata(false));

        public Measurement Selection
        {
            get { return (Measurement)GetValue(SelectionProperty); }
            set { SetValue(SelectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Selection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionProperty =
            DependencyProperty.Register("Selection", typeof(Measurement), typeof(MeasurementPicker),
                new PropertyMetadata(null));

        public double SelectedAmount
        {
            get { return (double)GetValue(SelectedAmountProperty); }
            set { SetValue(SelectedAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedAmount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedAmountProperty =
            DependencyProperty.Register("SelectedAmount", typeof(double), typeof(MeasurementPicker),
                new PropertyMetadata(0.0));

        public MetricPrefix SelectedPrefix
        {
            get { return (MetricPrefix)GetValue(SelectedPrefixProperty); }
            set { SetValue(SelectedPrefixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedPrefix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPrefixProperty =
            DependencyProperty.Register("SelectedPrefix", typeof(MetricPrefix), typeof(MeasurementPicker),
                new PropertyMetadata(null));

        public Unit SelectedUnit
        {
            get { return (Unit)GetValue(SelectedUnitProperty); }
            set { SetValue(SelectedUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedUnitProperty =
            DependencyProperty.Register("SelectedUnit", typeof(Unit), typeof(MeasurementPicker),
                new PropertyMetadata(null));
        #endregion

        #region User Interaction
        private void PlainTextText_LostFocus(object sender, RoutedEventArgs e) => ExitCodeEditingMode();

        private void PlainTextText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                ExitCodeEditingMode();
        }
        #endregion
    }
}
