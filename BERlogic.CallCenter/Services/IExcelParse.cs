using System.Collections.Generic;
using System.Threading.Tasks;

namespace BERlogic.CallCenter.Services
{
    public interface IExcelParse
    {
        Task<string> OnPostFileReportImport(string file, string connectionString, Dictionary<string, object> optionalParams);
        Task<IList<string>> OnPostFileReportImport(IList<string> files, string connectionString, Dictionary<string, object> optionalParams);
    }
}
