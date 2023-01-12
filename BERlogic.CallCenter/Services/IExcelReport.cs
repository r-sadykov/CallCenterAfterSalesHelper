using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;

namespace BERlogic.CallCenter.Services
{
    public interface IExcelReport
    {
        Task<byte[]> Export(DataTable dataTable, string fileName, List<string> typeList);
        ISheet Import(IFormFile file);
    }
}
