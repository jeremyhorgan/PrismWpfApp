using Prism.Commands;
using PrismWpfApp.Repository;

namespace PrismWpfApp.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly IExcelRepository _excelRepository;

        public MainWindowViewModel(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;

            FileOpenCommand = new DelegateCommand(FileOpen, CanFileOpen);
            SaveOpenCommand = new DelegateCommand(FileSave, CanFileSave);
        }

        public DelegateCommand FileOpenCommand { get; set; }

        public DelegateCommand SaveOpenCommand { get; set; }

        private void FileOpen()
        {
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
