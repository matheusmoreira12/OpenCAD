using System.Windows;
using System.Windows.Controls;

namespace ComponentSymbolAssistant
{
    public class WizardStep : DependencyObject
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(WizardStep), new PropertyMetadata(null));

        public Page Page
        {
            get { return (Page)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(Page), typeof(WizardStep), new PropertyMetadata(null));
    }
}
