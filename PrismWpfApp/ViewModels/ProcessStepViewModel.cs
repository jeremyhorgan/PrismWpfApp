using System.Windows.Threading;
using Prism.Events;
using Prism.Mvvm;
using PrismWpfApp.Infrastructure.Events;

namespace PrismWpfApp.ViewModels
{
    public class ProcessStepViewModel : BindableBase
    {
        private double _stepProgress;

        public ProcessStepViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator?.GetEvent<ExecuteStepProgressEvent>().Subscribe(OnProgressUpdate);
        }

        public int StepNumber { get; set; }

        public double StepProgress
        {
            get => _stepProgress;
            set => SetProperty(ref _stepProgress, value);
        }

        private void OnProgressUpdate(ExecuteStepProgressEventArgs e)
        {
            if (e.StepNumber == StepNumber)
            {
                Dispatcher.CurrentDispatcher.Invoke(() => StepProgress = e.Progress, DispatcherPriority.Background);
            }
        }
    }
}