using System.Windows;
using System.Windows.Controls.Primitives;

namespace UserInterface
{
    /// <summary>
    /// Interação lógica para ClarificationPopuo.xam
    /// </summary>
    public partial class DisambiguationPopup : Popup
    {
        public object Message
        {
            get { return (object)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(object), typeof(DisambiguationPopup),
                new PropertyMetadata(null));

        public DataTemplate MessageTemplate
        {
            get { return (DataTemplate)GetValue(MessageTemplateProperty); }
            set { SetValue(MessageTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageTemplateProperty =
            DependencyProperty.Register("MessageTemplate", typeof(DataTemplate), typeof(DisambiguationPopup),
                new PropertyMetadata(null));


        public UIElement AmbiguousItems
        {
            get { return (UIElement)GetValue(AmbiguousItemsProperty); }
            set { SetValue(AmbiguousItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AmbiguousItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmbiguousItemsProperty =
            DependencyProperty.Register("AmbiguousItems", typeof(UIElement), typeof(DisambiguationPopup),
                new PropertyMetadata(null));

        public DataTemplate AmbiguousTermTemplate
        {
            get { return (DataTemplate)GetValue(AmbiguousTermTemplateProperty); }
            set { SetValue(AmbiguousTermTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AmbiguousTermTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmbiguousTermTemplateProperty =
            DependencyProperty.Register("AmbiguousTermTemplate", typeof(DataTemplate),
                typeof(DisambiguationPopup), new PropertyMetadata(null));


        public DisambiguationPopup()
        {
            InitializeComponent();
        }
    }
}
