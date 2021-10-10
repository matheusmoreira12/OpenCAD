using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ComponentSymbolAssistant
{
    public class WizardController : DependencyObject
    {
        public int CurrentStepIndex { get; private set; }

        public static readonly DependencyProperty CurrentStepIndexProperty =
            DependencyProperty.Register("CurrentStepIndex", typeof(int), typeof(WizardController), new PropertyMetadata(-1));

        public Page CurrentPage
        {
            get { return (Page)GetValue(CurrentPageProperty); }
            protected set { SetValue(CurrentPagePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey CurrentPagePropertyKey =
            DependencyProperty.RegisterReadOnly("CurrentPage", typeof(Page), typeof(WizardController),
                new FrameworkPropertyMetadata(default(Page), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty CurrentPageProperty
            = CurrentPagePropertyKey.DependencyProperty;

        public List<WizardStep> Steps
        {
            get { return (List<WizardStep>)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public static readonly DependencyProperty StepsProperty =
            DependencyProperty.Register("Steps", typeof(List<WizardStep>), typeof(WizardController), new PropertyMetadata(new List<WizardStep> { }));

        public Page StartPage
        {
            get { return (Page)GetValue(StartPageProperty); }
            set { SetValue(StartPageProperty, value); }
        }

        public static readonly DependencyProperty StartPageProperty =
            DependencyProperty.Register("StartPage", typeof(Page), typeof(WizardController), new PropertyMetadata(null));

        public Page EndPage
        {
            get { return (Page)GetValue(EndPageProperty); }
            set { SetValue(EndPageProperty, value); }
        }

        public static readonly DependencyProperty EndPageProperty =
            DependencyProperty.Register("EndPage", typeof(Page), typeof(WizardController), new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == CurrentStepIndexProperty)
                UpdateCurrentStep();
        }

        private void UpdateCurrentStep()
        {
            int currentStepIndex = CurrentStepIndex;
            Page page;
            if (currentStepIndex < 0)
                page = StartPage;
            else if (currentStepIndex >= Steps.Count)
                page = EndPage;
            else
            {
                WizardStep step = Steps[currentStepIndex];
                page = step.Page;
            }
            CurrentPage = page;
        }
    }
}
