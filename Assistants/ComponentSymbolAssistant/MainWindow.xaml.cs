using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ComponentSymbolAssistant
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public WizardController WizardController
        {
            get { return (WizardController)GetValue(WizardControllerProperty); }
            protected set { SetValue(WizardControllerPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey WizardControllerPropertyKey =
             DependencyProperty.RegisterReadOnly(nameof(WizardController), typeof(WizardController), typeof(MainWindow),
                new FrameworkPropertyMetadata(default(WizardController), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty WizardControllerProperty
            = WizardControllerPropertyKey.DependencyProperty;

        public List<WizardStep> WizardSteps;

        public Page StartPage;

        public Page EndPage;

        public MainWindow()
        {
            InitializeComponent();

            CreateWizardController();
        }

        private void CreateWizardController()
        {
            WizardController = new WizardController()
            {
                StartPageSource = new Uri(@"\Views\WelcomePage.xaml", UriKind.RelativeOrAbsolute),

                Steps = new List<WizardStep>
                {
                    new WizardStep() {
                        PageSource = null,
                        Title = "Step 1",
                    },
                    new WizardStep() {
                        PageSource = null,
                        Title = "Step 2",
                    },
                    new WizardStep() {
                        PageSource = null,
                        Title = "Step 3",
                    },
                },
            };
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            WizardController.Next();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            WizardController.Previous();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            WizardController.Next();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            WizardController.Next();
        }
    }
}
