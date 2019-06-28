using Moq;
using Prism.Events;
using Prism.Logging;
using PrismWpfApp.Repository;
using PrismWpfApp.ViewModels;
using Xunit;

namespace PrismWpfApp.Test
{
    public class TestMainWindowViewModel
    {
        [Fact]
        private void ExecutingFileSaveCommand_InvokesExcelRepositorySave()
        {
            // Arrange
            var eventAggregator = new Mock<IEventAggregator>();
            var logger = new Mock<ILoggerFacade>();
            var excelRepository = new Mock<IExcelRepository>();

            // Act
            var vm = new MainWindowViewModel(eventAggregator.Object, logger.Object, excelRepository.Object);
            vm.SaveOpenCommand.Execute();
            vm.Dispose();

            // Assert
            excelRepository.Verify(m => m.Save());
        }
    }
}