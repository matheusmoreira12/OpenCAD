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

namespace UserInterface.Controls
{
    /// <summary>
    /// Interação lógica para SmartTextBox.xam
    /// </summary>
    public partial class HintTextBox : TextBox
    {
        public object HintText
        {
            get { return GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register("HintText", typeof(object), typeof(HintTextBox));

        public HintTextBox()
        {
            InitializeComponent();
        }

        public void InputTextDynamically(string text)
        {
            int caret = CaretIndex; //Store caret index

            //Insert desired text
            Text = Text.Insert(caret, text);

            //Adance caret
            caret += text.Length;

            //Write caret position
            CaretIndex = caret;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            SelectAll();
        }

        private void HideHint()
        {
            if (HintHost == null) return;

            HintHost.Visibility = Visibility.Hidden;
        }

        private void ShowHint()
        {
            if (HintHost == null) return;

            HintHost.Visibility = Visibility.Visible;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (string.IsNullOrEmpty(Text)) ShowHint(); else HideHint();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected ScrollViewer ContentHost => GetTemplateChild("PART_ContentHost") as ScrollViewer;
        protected TextBlock HintHost => GetTemplateChild("HintHost") as TextBlock;

    }
}
