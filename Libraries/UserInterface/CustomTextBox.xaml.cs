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

namespace UserInterface
{
    /// <summary>
    /// Interação lógica para SmartTextBox.xam
    /// </summary>
    public partial class CustomTextBox : TextBox
    {
        public string PlaceHolderText
        {
            get { return (string)GetValue(PlaceHolderTextProperty); }
            set { SetValue(PlaceHolderTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolderText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderTextProperty =
            DependencyProperty.Register("PlaceHolderText", typeof(string), typeof(CustomTextBox));

        public CustomTextBox()
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

        private void HidePlaceHolder()
        {
            if (PlaceHolderHost == null) return;

            PlaceHolderHost.Visibility = Visibility.Hidden;
        }

        private void ShowPlaceHolder()
        {
            if (PlaceHolderHost == null) return;

            PlaceHolderHost.Visibility = Visibility.Visible;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (string.IsNullOrEmpty(Text)) ShowPlaceHolder(); else HidePlaceHolder();
        }

        protected ScrollViewer ContentHost => GetTemplateChild("PART_ContentHost") as ScrollViewer;
        protected TextBlock PlaceHolderHost => GetTemplateChild("PlaceHolderHost") as TextBlock;

    }
}
