using System.Threading;
using Prism.Events;
using Prism.Logging;
using PrismWpfApp.Infrastructure.Events;

namespace PrismWpfApp.ProcessSteps
{
    public class ExecuteCreateZipFileProcessStep : IExecuteProcessStep
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly int _stepNumber;

        public ExecuteCreateZipFileProcessStep(IEventAggregator eventAggregator, ILoggerFacade logger, int stepNumber)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
            _stepNumber = stepNumber;
        }

        public void Run(CancellationToken token)
        {
            var count = 0;
            while (!token.IsCancellationRequested && count++ < 10)
            {
                // TODO: check if IEventAggregator instance is thread safe
                var progressEvent = _eventAggregator.GetEvent<ExecuteStepProgressEvent>();
                progressEvent.Publish(new ExecuteStepProgressEventArgs(_stepNumber, count * 10));

                _logger.Log($"Creating zip files...{count}", Category.Debug, Priority.None);
                Thread.Sleep(500);
            }

            _logger.Log("Completed creating zip files", Category.Debug, Priority.None);
        }
    }
}