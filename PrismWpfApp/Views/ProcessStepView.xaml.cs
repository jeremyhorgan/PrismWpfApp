using System.Windows;
using System.Windows.Controls;
using PrismWpfApp.ViewModels;

namespace PrismWpfApp.Views
{
    /// <summary>
    /// Interaction logic for ProcessStepView.xaml.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ProcessStepView : UserControl
    {
        public static readonly DependencyProperty StepNumberProperty =
            DependencyProperty.Register("StepNumber", typeof(int), typeof(ProcessStepView), new FrameworkPropertyMetadata(OnStepNumberChanged));

        public static readonly DependencyProperty StepNameProperty =
            DependencyProperty.Register("StepName", typeof(string), typeof(ProcessStepView), new PropertyMetadata(default(string)));

        public ProcessStepView()
        {
            InitializeComponent();
        }

        public int StepNumber
        {
            get => (int)GetValue(StepNumberProperty);
            set => SetValue(StepNumberProperty, value);
        }

        public string StepName
        {
            get => (string)GetValue(StepNameProperty);
            set => SetValue(StepNameProperty, value);
        }

        private static void OnStepNumberChanged(DependencyObject control, DependencyPropertyChangedEventArgs e)
        {
            var processStepView = (ProcessStepView)control;

            var viewModel = (ProcessStepViewModel)processStepView.DataContext;
            viewModel.StepNumber = (int)e.NewValue;
        }
    }
}
