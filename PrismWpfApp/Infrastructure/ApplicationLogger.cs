using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
