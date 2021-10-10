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
            CreateAndBindRowAndColumnDefinitions();
            BindMargin();
        }

        private void MakeHitTestVisibleInChrome() =>
            SetValue(WindowChrome.IsHitTestVisibleInChromeProperty, true);

        private void CreateAndBindRowAndColumnDefinitions()
        {
            if (ParentWindow == null)
                return;

            RowDefinitions.Clear();

            var row0 = new RowDefinition();
            RowDefinitions.Add(row0);

            var row1 = new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) };
            RowDefinitions.Add(row1);

            var row2 = new RowDefinition();
            RowDefinitions.Add(row2);

            var column0 = new ColumnDefinition();
            ColumnDefinitions.Add(column0);

            var column1 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
            ColumnDefinitions.Add(column1);

            var column2 = new ColumnDefinition();
            ColumnDefinitions.Add(column2);

            row0.SetBinding(RowDefinition.HeightProperty, new Binding()
                {
                    Path = new PropertyPath("GlassFrameThickness.Top"),
                    Source = ParentWindow,
                }
            );

            row2.SetBinding(RowDefinition.HeightProperty, new Binding()
                {
                    Path = new PropertyPath("GlassFrameThickness.Bottom"),
                    Source = ParentWindow,
                }
            );

            column0.SetBinding(ColumnDefinition.WidthProperty, new Binding()
                {
                    Path = new PropertyPath("GlassFrameThickness.Left"),
                    Source = ParentWindow,
                }
            );

            column2.SetBinding(ColumnDefinition.WidthProperty, new Binding()
                {
                    Path = new PropertyPath("GlassFrameThickness.Right"),
                    Source = ParentWindow,
                }
            );
        }

        private void BindMargin()
        {
            if (ParentWindow == null)
                return;

            BindingOperations.ClearBinding(this, MarginProperty);

            SetBinding(MarginProperty, new Binding()
            {
                Path = new PropertyPath("GlassFrameThickness"),
                Converter = new ComputeMarginGlassFrameThickness(),
                Source = ParentWindow,
            });
        }

        private CustomWindow ParentWindow;
    }
}
