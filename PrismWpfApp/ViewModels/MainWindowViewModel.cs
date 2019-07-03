using System;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using PrismWpfApp.Infrastructure;
using PrismWpfApp.Model;
using PrismWpfApp.ProcessSteps;
using PrismWpfApp.Repository;

namespace PrismWpfApp.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        private readonly IExcelRepository _excelRepository;
        private readonly IOpenFileDialogService _openFileDialogService;
        private readonly ILoggerFacade _logger;
        private readonly IEventAggregator _eventAggregator;
        private readonly ApplicationState _applicationState;
        private CancellationTokenSource _cancellationToken;
        private bool _isRunning;

        public MainWindowViewModel(
            IEventAggregator eventAggregator,
            ILoggerFacade logger,
            IExcelRepository excelRepository,
            IOpenFileDialogService openFileDialogService,
            ApplicationState applicationState)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
            _excelRepository = excelRepository;
            _openFileDialogService = openFileDialogService;
            _applicationState = applicationState;

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
                FileOpenCommand.RaiseCanExecuteChanged();
            });
        }

        private void FileOpen()
        {
            var filename = _openFileDialogService.Show();
            if (!string.IsNullOrEmpty(filename))
            {
                _applicationState.ExcelFilename = filename;
                RunCloudCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanFileOpen()
        {
            return !_isRunning;
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
            FileOpenCommand.RaiseCanExecuteChanged();

            _cancellationToken = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(DoWork, _cancellationToken);
        }

        private bool CanRunCloud()
        {
            return !_isRunning && File.Exists(_applicationState.ExcelFilename);
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
