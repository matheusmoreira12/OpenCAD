using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shell;

namespace ComponentSymbolAssistant
{
    public sealed class CustomWindowGrid : Grid
    {
        private class ComputeMarginGlassFrameThickness : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is Thickness)
                {
                    Thickness glassFrameThickness = (Thickness)value;
                    return new Thickness(-glassFrameThickness.Left, -glassFrameThickness.Top,
                        -glassFrameThickness.Right, -glassFrameThickness.Bottom);
                }
                throw new ArgumentException(null, nameof(value));
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
                throw new NotImplementedException();
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            UpdateParentAndSetup();
        }

        private void UpdateParentAndSetup()
        {
            ParentWindow = Parent as CustomWindow;
            Setup();
        }

        private void Setup()
        {
            MakeHitTestVisibleInChrome();
            CreateRowAndColumnDefinitions();
            BindRowAndColumnDefinitions();
            BindMargin();
        }

        private void MakeHitTestVisibleInChrome() =>
            SetValue(WindowChrome.IsHitTestVisibleInChromeProperty, true);

        private void CreateRowAndColumnDefinitions()
        {
            if (ParentWindow == null)
                return;

            RowDefinitions.Clear();

            row0 = new RowDefinition();
            RowDefinitions.Add(row0);

            row1 = new RowDefinition();
            row1.Height = new GridLength(1, GridUnitType.Star);
            RowDefinitions.Add(row1);

            row2 = new RowDefinition();
            RowDefinitions.Add(row2);

            column0 = new ColumnDefinition();
            ColumnDefinitions.Add(column0);

            column1 = new ColumnDefinition();
            column1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinitions.Add(column1);

            column2 = new ColumnDefinition();
            ColumnDefinitions.Add(column2);
        }

        private void BindRowAndColumnDefinitions()
        {
            if (row0 == null ||
                row1 == null ||
                row2 == null ||
                column0 == null ||
                column1 == null ||
                column2 == null)
                return;

            BindingOperations.ClearBinding(row0, RowDefinition.HeightProperty);
            BindingOperations.ClearBinding(row2, RowDefinition.HeightProperty);
            BindingOperations.ClearBinding(column0, ColumnDefinition.WidthProperty);
            BindingOperations.ClearBinding(column2, ColumnDefinition.WidthProperty);

            row0HeightBinding = new Binding();
            row0HeightBinding.Path = new PropertyPath("GlassFrameThickness.Top");
            row0HeightBinding.Source = ParentWindow;
            row0.SetBinding(RowDefinition.HeightProperty, row0HeightBinding);

            row2HeightBinding = new Binding();
            row2HeightBinding.Path = new PropertyPath("GlassFrameThickness.Bottom");
            row2HeightBinding.Source = ParentWindow;
            row2.SetBinding(RowDefinition.HeightProperty, row2HeightBinding);

            column0WidthBinding = new Binding();
            column0WidthBinding.Path = new PropertyPath("GlassFrameThickness.Left");
            column0WidthBinding.Source = ParentWindow;
            column0.SetBinding(ColumnDefinition.WidthProperty, column0WidthBinding);

            column2WidthBinding = new Binding();
            column2WidthBinding.Path = new PropertyPath("GlassFrameThickness.Right");
            column2WidthBinding.Source = ParentWindow;
            column2.SetBinding(ColumnDefinition.WidthProperty, column2WidthBinding);
        }

        private void BindMargin()
        {
            if (ParentWindow == null)
                return;

            BindingOperations.ClearBinding(this, MarginProperty);

            marginBinding = new Binding();
            marginBinding.Path = new PropertyPath("GlassFrameThickness");
            marginBinding.Converter = new ComputeMarginGlassFrameThickness();
            marginBinding.Source = ParentWindow;
            SetBinding(MarginProperty, marginBinding);
        }

        private RowDefinition row0;
        private RowDefinition row1;
        private RowDefinition row2;
        private ColumnDefinition column0;
        private ColumnDefinition column1;
        private ColumnDefinition column2;
        private Binding row0HeightBinding;
        private Binding row2HeightBinding;
        private Binding column0WidthBinding;
        private Binding column2WidthBinding;
        private Binding marginBinding;
        private CustomWindow ParentWindow;
    }
}
