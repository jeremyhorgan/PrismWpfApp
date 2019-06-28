using System;
using System.Threading;
using System.Windows.Threading;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using PrismWpfApp.ProcessSteps;
using PrismWpfApp.Repository;

namespace PrismWpfApp.ViewModels
{
    public class MainWindowViewModel : IDisposable
    {
        private readonly IExcelRepository _excelRepository;
        private readonly ILoggerFacade _logger;
        private readonly IEventAggregator _eventAggregator;
        private CancellationTokenSource _cancellationToken;
        private bool _isRunning;

        public MainWindowViewModel(IEventAggregator eventAggregator, ILoggerFacade logger, IExcelRepository excelRepository)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
            _excelRepository = excelRepository;

            FileOpenCommand = new DelegateCommand(FileOpen, CanFileOpen);
            SaveOpenCommand = new DelegateCommand(FileSave, CanFileSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            RunCloudCommand = new DelegateCommand(RunCloud, CanRunCloud);
        }

        public DelegateCommand FileOpenCommand { get; set; }

        public DelegateCommand SaveOpenCommand { get; set; }

        public DelegateCommand RunCloudCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _cancellationToken?.Dispose();
        }

        private void DoWork(object state)
        {
            var cancellationToken = ((CancellationTokenSource)state).Token;

            // Execute the process steps
            var step1 = new ExecuteExcelToCsvProcessStep(_eventAggregator, _logger, 1);
            step1.Run(cancellationToken);

            var step2 = new ExecuteCreateZipFileProcessStep(_eventAggregator, _logger, 2);
            step2.Run(cancellationToken);

            // Update the UI command state
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                _isRunning = false;
                RunCloudCommand.RaiseCanExecuteChanged();
                CancelCommand.RaiseCanExecuteChanged();
            });
        }

        private void FileOpen()
        {
            _logger.Log("File open command", Category.Debug, Priority.None);

            // TODO: Implement IOpenFileDialogService https://github.com/PrismLibrary/Prism/issues/1666
        }

        private bool CanFileOpen()
        {
            return false;
        }

        private void FileSave()
        {
            _excelRepository.Save();
        }

        private bool CanFileSave()
        {
            return false;
        }

        private void RunCloud()
        {
            _isRunning = true;
            CancelCommand.RaiseCanExecuteChanged();

            _cancellationToken = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(DoWork, _cancellationToken);
        }

        private bool CanRunCloud()
        {
            return !_isRunning;
        }

        private void Cancel()
        {
            _cancellationToken.Cancel();
        }

        private bool CanCancel()
        {
            return _isRunning;
        }
    }
}
