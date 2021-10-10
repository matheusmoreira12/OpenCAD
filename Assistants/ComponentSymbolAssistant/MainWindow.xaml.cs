using System;
using System.Windows;

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
            DependencyProperty.RegisterReadOnly("WizardController", typeof(WizardController), typeof(MainWindow),
                new FrameworkPropertyMetadata(default(WizardController), FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty WizardControllerProperty
            = WizardControllerPropertyKey.DependencyProperty;

        public MainWindow()
        {
            InitializeComponent();

            CreateWizardController();
        }

        private void CreateWizardController()
        {
            WizardController = new WizardController();
        }
    }
}
