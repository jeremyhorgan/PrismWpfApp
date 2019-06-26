using System;
using System.Threading;
using Prism.Commands;
using Prism.Logging;
using PrismWpfApp.Repository;

namespace PrismWpfApp.ViewModels
{
    public class MainWindowViewModel : IDisposable
    {
        private readonly IExcelRepository _excelRepository;
        private readonly ILoggerFacade _logger;
        private readonly CancellationTokenSource _cancellationToken;

        public MainWindowViewModel(IExcelRepository excelRepository, ILoggerFacade logger)
        {
            _excelRepository = excelRepository;
            _logger = logger;

            FileOpenCommand = new DelegateCommand(FileOpen, CanFileOpen);
            SaveOpenCommand = new DelegateCommand(FileSave, CanFileSave);

            _cancellationToken = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(DoWork, _cancellationToken);
        }

        public DelegateCommand FileOpenCommand { get; set; }

        public DelegateCommand SaveOpenCommand { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _cancellationToken.Dispose();
        }

        private void DoWork(object state)
        {
            var cancellationToken = ((CancellationTokenSource)state).Token;
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.Log("Hello", Category.Debug, Priority.None);
                Thread.Sleep(2000);
            }
        }

        private void FileOpen()
        {
            _logger.Log("File open command", Category.Debug, Priority.None);

            _cancellationToken.Cancel();

            // TODO: Implement IOpenFileDialogService https://github.com/PrismLibrary/Prism/issues/1666
        }

        private bool CanFileOpen()
        {
            return true;
        }

        private void FileSave()
        {
            _excelRepository.Save();
        }

        private bool CanFileSave()
        {
            return true;
        }
    }
}
