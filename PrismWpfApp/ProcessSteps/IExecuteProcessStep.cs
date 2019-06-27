using System.Threading;

namespace PrismWpfApp.ProcessSteps
{
    public interface IExecuteProcessStep
    {
        void Run(CancellationToken token);
    }
}