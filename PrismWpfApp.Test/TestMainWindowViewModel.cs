using System.Reflection;
using Moq;
using Prism.Events;
using Prism.Logging;
using PrismWpfApp.Infrastructure;
using PrismWpfApp.Model;
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
            var openFileDialogService = new Mock<IOpenFileDialogService>();
            var applicationState = new ApplicationState();

            // Act
            var vm = new MainWindowViewModel(eventAggregator.Object, logger.Object, excelRepository.Object, openFileDialogService.Object, applicationState);
            vm.SaveOpenCommand.Execute();
            vm.Dispose();

            // Assert
            excelRepository.Verify(m => m.Save());
        }

        [Fact]
        private void ExecutingFileOpenCommandWithValidFile_EnablesRunCommand()
        {
            // Arrange
            var eventAggregator = new Mock<IEventAggregator>();
            var logger = new Mock<ILoggerFacade>();
            var excelRepository = new Mock<IExcelRepository>();
            var openFileDialogService = new Mock<IOpenFileDialogService>();
            var applicationState = new ApplicationState();

            openFileDialogService.Setup(m => m.Show()).Returns(Assembly.GetExecutingAssembly().Location);

            // Act
            var vm = new MainWindowViewModel(eventAggregator.Object, logger.Object, excelRepository.Object, openFileDialogService.Object, applicationState);
            vm.FileOpenCommand.Execute();
            var canRun = vm.RunCloudCommand.CanExecute();
            vm.Dispose();

            // Assert
            Assert.True(canRun);
        }

        [Fact]
        private void ExecutingFileOpenCommandWithInValidFile_DisablesRunCommand()
        {
            // Arrange
            var eventAggregator = new Mock<IEventAggregator>();
            var logger = new Mock<ILoggerFacade>();
            var excelRepository = new Mock<IExcelRepository>();
            var openFileDialogService = new Mock<IOpenFileDialogService>();
            var applicationState = new ApplicationState();

            openFileDialogService.Setup(m => m.Show()).Returns(string.Empty);

            // Act
            var vm = new MainWindowViewModel(eventAggregator.Object, logger.Object, excelRepository.Object, openFileDialogService.Object, applicationState);
            var canRun = vm.RunCloudCommand.CanExecute();
            vm.Dispose();

            // Assert
            Assert.False(canRun);
        }
    }
}