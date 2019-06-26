using Prism.Commands;
using Prism.Logging;
using PrismWpfApp.Repository;

namespace PrismWpfApp.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly IExcelRepository _excelRepository;
        private readonly ILoggerFacade _logger;

        public MainWindowViewModel(IExcelRepository excelRepository, ILoggerFacade logger)
        {
            _excelRepository = excelRepository;
            _logger = logger;

            FileOpenCommand = new DelegateCommand(FileOpen, CanFileOpen);
            SaveOpenCommand = new DelegateCommand(FileSave, CanFileSave);
        }

        public DelegateCommand FileOpenCommand { get; set; }

        public DelegateCommand SaveOpenCommand { get; set; }

        private void FileOpen()
        {
            _logger.Log("File open command", Category.Debug, Priority.None);

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
