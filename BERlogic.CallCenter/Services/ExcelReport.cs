using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BERlogic.CallCenter.Services
{
    public class ExcelReport:IExcelReport
    {
        public async Task<byte[]> Export(DataTable dataTable, string fileName, List<string> typeList)
        {
            string sWebRootFolder = Environment.CurrentDirectory;
            string sFileName = fileName;
            var memory = new MemoryStream();
            string path = Path.Combine(sWebRootFolder, "wwwroot", "download", sFileName);
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("ServiceOperations");
                IRow row = excelSheet.CreateRow(0);

                var headerStyle = workbook.CreateCellStyle(); //Formatting
                var headerFont = workbook.CreateFont();
                headerFont.IsBold = true;
                headerStyle.SetFont(headerFont);

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    ICell header = row.CreateCell(i);
                    header.CellStyle = headerStyle;
                    header.SetCellValue(dataTable.Columns[i].ColumnName);
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    row = excelSheet.CreateRow(i + 1);
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        ICell row1 = row.CreateCell(j);

                        var currentCellValue = dataTable.Rows[i][j];

                        if (currentCellValue != null &&
                            !string.IsNullOrEmpty(Convert.ToString(currentCellValue)))
                        {
                            switch (typeList[j])
                            {
                                case "String":
                                    row1.SetCellValue(Convert.ToString(currentCellValue));
                                    break;
                                case "Int32":
                                    row1.SetCellValue(Convert.ToInt32(currentCellValue));
                                    break;
                                case "DateTime":
                                    row1.SetCellValue(Convert.ToString(currentCellValue));
                                    break;
                                case "Decimal":
                                    row1.SetCellValue(Convert.ToDouble(currentCellValue));
                                    break;
                                case "Double":
                                    row1.SetCellValue(Convert.ToDouble(currentCellValue));
                                    break;
                                default:
                                    row1.SetCellValue(Convert.ToString(currentCellValue));
                                    break;
                            }
                        }
                        else
                        {
                            row1.SetCellValue(string.Empty);
                        }
                    }
                }
                workbook.Write(fs);
            }
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);                
            }
            memory.Position = 0;
            return memory.ToArray();
        }

        public ISheet Import(IFormFile file)
        {
            return null;
        }
    }
}
