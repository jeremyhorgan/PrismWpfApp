using Microsoft.Win32;

namespace PrismWpfApp.Infrastructure
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public string Show()
        {
            var filename = string.Empty;

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filename = openFileDialog.FileName;
            }

            return filename;
        }
    }
}