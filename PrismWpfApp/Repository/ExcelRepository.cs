using System.Diagnostics;

namespace PrismWpfApp.Repository
{
    public class ExcelRepository : IExcelRepository
    {
        public void Save()
        {
            Trace.WriteLine("Excel Save");
        }
    }
}
