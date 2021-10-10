using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ComponentSymbolAssistant
{
    public class WizardController : DependencyObject
    {
        public WizardController()
        {
            SetCurrentStepIndex(-1);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == StepsProperty ||
                e.Property == StartPageSourceProperty ||
                e.Property == EndPageSourceProperty)
            {
                UpdateCurrentPage();
                UpdateNavigationStatus();
            }
        }

        public void Previous() => SetCurrentStepIndex(CurrentStepIndex - 1);

        public void Next() => SetCurrentStepIndex(CurrentStepIndex + 1);

        private void SetCurrentStepIndex(int newIndex)
        {
            if (newIndex < -1 || newIndex > Steps.Count)
                return;

            CurrentStepIndex = newIndex;
            UpdateCurrentPage();
            UpdateNavigationStatus();
        }

        private void UpdateCurrentPage()
        {
            CurrentPageSource = getCurrentPage();

            Uri getCurrentPage()
            {
                int currentStepIndex = CurrentStepIndex;
                if (currentStepIndex < 0)
                    return StartPageSource;
                if (currentStepIndex >= Steps.Count)
                    return EndPageSource;

                WizardStep currentStep = Steps[currentStepIndex];
                return currentStep.PageSource;
            }
        }

        private void UpdateNavigationStatus()
        {
            int currentStepIndex = CurrentStepIndex;
            int stepCount = Steps.Count;
            CanFinish = currentStepIndex == stepCount;
            CanStart = currentStepIndex == -1;
            CanGoForward = currentStepIndex >= 0 && currentStepIndex < stepCount;
            CanGoBackward = currentStepIndex >= 0;
        }

        public int CurrentStepIndex { get; private set; }

        public static readonly DependencyProperty CurrentStepIndexProperty =
            DependencyProperty.Register(nameof(CurrentStepIndex), typeof(int), typeof(WizardController), new PropertyMetadata(-1));

        public Uri CurrentPageSource
        {
            get { return (Uri)GetValue(CurrentPageSourceProperty); }
            protected set { SetValue(CurrentPageSourcePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey CurrentPageSourcePropertyKey =
             DependencyProperty.RegisterReadOnly(nameof(CurrentPageSource), typeof(Uri), typeof(WizardController),
                new FrameworkPropertyMetadata(default(Uri), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty CurrentPageSourceProperty
            = CurrentPageSourcePropertyKey.DependencyProperty;

        public bool CanGoForward
        {
            get { return (bool)GetValue(CanGoForwardProperty); }
            protected set { SetValue(CanGoForwardPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey CanGoForwardPropertyKey =
             DependencyProperty.RegisterReadOnly(nameof(CanGoForward), typeof(bool), typeof(WizardController),
                new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty CanGoForwardProperty
            = CanGoForwardPropertyKey.DependencyProperty;

        public bool CanGoBackward
        {
            get { return (bool)GetValue(CanGoBackwardProperty); }
            protected set { SetValue(CanGoBackwardPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey CanGoBackwardPropertyKey =
             DependencyProperty.RegisterReadOnly(nameof(CanGoBackward), typeof(bool), typeof(WizardController),
                new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty CanGoBackwardProperty
            = CanGoBackwardPropertyKey.DependencyProperty;

        public bool CanStart
        {
            get { return (bool)GetValue(CanStartProperty); }
            protected set { SetValue(CanStartPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey CanStartPropertyKey =
             DependencyProperty.RegisterReadOnly(nameof(CanStart), typeof(bool), typeof(WizardController),
                new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty CanStartProperty
            = CanStartPropertyKey.DependencyProperty;

        public bool CanFinish
        {
            get { return (bool)GetValue(CanFinishProperty); }
            protected set { SetValue(CanFinishPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey CanFinishPropertyKey =
             DependencyProperty.RegisterReadOnly(nameof(CanFinish), typeof(bool), typeof(WizardController),
                new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty CanFinishProperty
            = CanFinishPropertyKey.DependencyProperty;

        public List<WizardStep> Steps
        {
            get { return (List<WizardStep>)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public static readonly DependencyProperty StepsProperty =
            DependencyProperty.Register(nameof(Steps), typeof(List<WizardStep>), typeof(WizardController), new PropertyMetadata(new List<WizardStep> { }));

        public Uri StartPageSource
        {
            get { return (Uri)GetValue(StartPageSourceProperty); }
            set { SetValue(StartPageSourceProperty, value); }
        }

        public static readonly DependencyProperty StartPageSourceProperty =
            DependencyProperty.Register(nameof(StartPageSource), typeof(Uri), typeof(WizardController), new PropertyMetadata(null));

        public Uri EndPageSource
        {
            get { return (Uri)GetValue(EndPageSourceProperty); }
            set { SetValue(EndPageSourceProperty, value); }
        }

        public static readonly DependencyProperty EndPageSourceProperty =
            DependencyProperty.Register(nameof(EndPageSource), typeof(Uri), typeof(WizardController), new PropertyMetadata(null));
    }
}
