using System.Diagnostics;
using Prism.Logging;

namespace PrismWpfApp.Infrastructure
{
    public class ApplicationLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            Trace.WriteLine(message);
        }
    }
}
