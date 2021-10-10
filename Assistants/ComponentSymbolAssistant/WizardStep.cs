using System;
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
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(WizardStep), new PropertyMetadata(null));

        public Uri PageSource
        {
            get { return (Uri)GetValue(PageSourceProperty); }
            set { SetValue(PageSourceProperty, value); }
        }

        public static readonly DependencyProperty PageSourceProperty =
            DependencyProperty.Register(nameof(PageSource), typeof(Uri), typeof(WizardStep), new PropertyMetadata(null));
    }
}
