namespace PrismWpfApp.Infrastructure.Events
{
    public class ExecuteStepProgressEventArgs
    {
        public ExecuteStepProgressEventArgs(int stepNumber, double progress)
        {
            StepNumber = stepNumber;
            Progress = progress;
        }

        public int StepNumber { get; private set; }

        public double Progress { get; private set; }
    }
}