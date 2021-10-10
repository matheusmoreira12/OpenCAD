using System.Windows;
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
            DependencyProperty.Register("UseAeroCaptionButtons", typeof(bool), typeof(CustomWindow));

        public int CaptionHeight
        {
            get { return (int)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(int), typeof(CustomWindow), new PropertyMetadata(0));

        public Thickness GlassFrameThickness
        {
            get { return (Thickness)GetValue(GlassFrameThicknessProperty); }
            set { SetValue(GlassFrameThicknessProperty, value); }
        }

        public static readonly DependencyProperty GlassFrameThicknessProperty =
            DependencyProperty.Register("GlassFrameThickness", typeof(Thickness), typeof(CustomWindow), new PropertyMetadata(new Thickness()));

        public Thickness ResizeBorderThickness
        {
            get { return (Thickness)GetValue(ResizeBorderThicknessProperty); }
            set { SetValue(ResizeBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.Register("ResizeBorderThickness", typeof(Thickness), typeof(CustomWindow), new PropertyMetadata(new Thickness()));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomWindow), new PropertyMetadata(new CornerRadius()));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            CreateWindowChrome();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == UseAeroCaptionButtonsProperty ||
                e.Property == CaptionHeightProperty ||
                e.Property == GlassFrameThicknessProperty ||
                e.Property == ResizeBorderThicknessProperty ||
                e.Property == CornerRadiusProperty)
            {
                UpdateWindowChrome();
                InvalidateVisual();
            }
        }

        private void CreateWindowChrome()
        {
            if (WindowChrome != null)
                return;

            WindowChrome windowChrome = new WindowChrome();
            SetValue(WindowChrome.WindowChromeProperty, windowChrome);
            WindowChrome = windowChrome;

            UpdateWindowChrome();
        }

        private void UpdateWindowChrome()
        {
            if (WindowChrome == null)
                return;

            WindowChrome.UseAeroCaptionButtons = UseAeroCaptionButtons;
            WindowChrome.CaptionHeight = CaptionHeight;
            WindowChrome.GlassFrameThickness = GlassFrameThickness;
            WindowChrome.ResizeBorderThickness = ResizeBorderThickness;
            WindowChrome.CornerRadius = CornerRadius;
        }

        protected WindowChrome WindowChrome { get; private set; } = null;
    }
}