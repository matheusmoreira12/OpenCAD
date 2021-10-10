using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shell;

namespace ComponentSymbolAssistant
{
    public class CustomWindow : Window
    {
        static CustomWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
        }

        public bool UseAeroCaptionButtons
        {
            get { return (bool)GetValue(UseAeroCaptionButtonsProperty); }
            set { SetValue(UseAeroCaptionButtonsProperty, value); }
        }

        public static readonly DependencyProperty UseAeroCaptionButtonsProperty =
            DependencyProperty.Register(nameof(UseAeroCaptionButtons), typeof(bool), typeof(CustomWindow));

        public int CaptionHeight
        {
            get { return (int)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register(nameof(CaptionHeight), typeof(int), typeof(CustomWindow), new PropertyMetadata(0));

        public Thickness GlassFrameThickness
        {
            get { return (Thickness)GetValue(GlassFrameThicknessProperty); }
            set { SetValue(GlassFrameThicknessProperty, value); }
        }

        public static readonly DependencyProperty GlassFrameThicknessProperty =
            DependencyProperty.Register(nameof(GlassFrameThickness), typeof(Thickness), typeof(CustomWindow), new PropertyMetadata(new Thickness()));

        public Thickness ResizeBorderThickness
        {
            get { return (Thickness)GetValue(ResizeBorderThicknessProperty); }
            set { SetValue(ResizeBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.Register(nameof(ResizeBorderThickness), typeof(Thickness), typeof(CustomWindow), new PropertyMetadata(new Thickness()));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CustomWindow), new PropertyMetadata(new CornerRadius()));

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            CreateWindowChrome();
        }

        private void CreateWindowChrome()
        {
            if (WindowChrome != null)
                return;

            WindowChrome = new WindowChrome();
            SetValue(WindowChrome.WindowChromeProperty, WindowChrome);

            BindWindowChrome();
        }

        private void BindWindowChrome()
        {
            if (WindowChrome == null)
                return;

            BindingOperations.ClearBinding(WindowChrome, WindowChrome.UseAeroCaptionButtonsProperty);
            BindingOperations.ClearBinding(WindowChrome, WindowChrome.CaptionHeightProperty);
            BindingOperations.ClearBinding(WindowChrome, WindowChrome.GlassFrameThicknessProperty);
            BindingOperations.ClearBinding(WindowChrome, WindowChrome.ResizeBorderThicknessProperty);
            BindingOperations.ClearBinding(WindowChrome, WindowChrome.CornerRadiusProperty);

            Binding useAeroCaptionButtonsBinding = new Binding()
            {
                Path = new PropertyPath(UseAeroCaptionButtonsProperty),
                Source = this,
            };
            BindingOperations.SetBinding(WindowChrome, WindowChrome.UseAeroCaptionButtonsProperty, useAeroCaptionButtonsBinding);

            Binding captionHeightBinding = new Binding()
            {
                Path = new PropertyPath(CaptionHeightProperty),
                Source = this,
            };
            BindingOperations.SetBinding(WindowChrome, WindowChrome.CaptionHeightProperty, captionHeightBinding);

            Binding glassFrameThicknessBinding = new Binding()
            {
                Path = new PropertyPath(GlassFrameThicknessProperty),
                Source = this,
            };
            BindingOperations.SetBinding(WindowChrome, WindowChrome.GlassFrameThicknessProperty, glassFrameThicknessBinding);

            Binding resizeBorderThicknessBinding = new Binding()
            {
                Path = new PropertyPath(ResizeBorderThicknessProperty),
                Source = this,
            };
            BindingOperations.SetBinding(WindowChrome, WindowChrome.ResizeBorderThicknessProperty, resizeBorderThicknessBinding);

            Binding cornerRadiusBinding = new Binding()
            {
                Path = new PropertyPath(CornerRadiusProperty),
                Source = this,
            };
            BindingOperations.SetBinding(WindowChrome, WindowChrome.CornerRadiusProperty, cornerRadiusBinding);
        }

        protected WindowChrome WindowChrome { get; private set; } = null;
    }
}