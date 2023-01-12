using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BERlogic.CallCenter.Extensions;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BERlogic.CallCenter.Services
{
    public class ExcelParse : IExcelParse
    {
        public async Task<string> OnPostFileReportImport(string file, string connectionString, Dictionary<string, object> optionalParams)
        {
            string uploadFolder = "upload";
            string currentProjectPath = Directory.GetCurrentDirectory();
            string webRootFolder = "wwwroot";

            string filePath = Path.Combine(currentProjectPath, webRootFolder, uploadFolder, optionalParams["UploadFolder"] as string);
            string successFilePath = filePath;
            if (optionalParams["SuccessFolder"] != null)
            {
                successFilePath = Path.Combine(currentProjectPath, webRootFolder, uploadFolder, optionalParams["UploadFolder"] as string, optionalParams["SuccessFolder"] as string);
            }


            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!Directory.Exists(successFilePath))
            {
                Directory.CreateDirectory(successFilePath);
            }

            if (file.Length > 0)
            {
                FullReportJournal journal = null;
                IFileReportJournal repository = null;
                IFullReport efReports = null;
                if (optionalParams["Journal"] != null && optionalParams["Repository"] != null)
                {
                    journal = new FullReportJournal();
                    repository = (IFileReportJournal)optionalParams["Journal"];
                    efReports = (IFullReport)optionalParams["Repository"];
                }
                string successFileReport = Path.Combine(successFilePath, Path.GetFileName(file));
                bool result = false;
                if (optionalParams["Model"].Equals("FullReport"))
                {
                    var reports = await GetFullReportData(file) as List<FullReport>;
                    if (journal != null)
                    {
                        journal.FileName = file;
                        journal.FileUploadDate = new FileInfo(file).LastWriteTime;
                        if (efReports?.FullReports != null)
                        {
                            var bookingNumber = efReports.FullReports.LastOrDefault()?.BookingNumber;
                            if (bookingNumber != null)
                                journal.BookingNumber_Before =
                                    (int)bookingNumber;
                            journal.Pnr_Before = efReports.FullReports.LastOrDefault()?.FirstGdsBookingNumber;
                        }
                        result = await BulkCopyToDatabase<FullReport>(reports, connectionString, optionalParams);
                        if (result)
                        {
                            journal.RegistrationEventDate = DateTime.Now;
                            var bookingNumber = efReports.FullReports?.LastOrDefault()?.BookingNumber;
                            if (bookingNumber != null)
                                journal.BookingNumber_After =
                                    (int)bookingNumber;
                            journal.Pnr_After = efReports.FullReports?.LastOrDefault()?.FirstGdsBookingNumber;
                            repository?.Save(journal);
                        }
                    }
                    else
                    {
                        result = await BulkCopyToDatabase<FullReport>(reports, connectionString, optionalParams);
                    }
                }

                if (!result) return $"{file} was failed to upload its data to Database";
                Directory.Move(file, successFileReport);
                return $"{file} was successful uploaded to Database";
            }
            return $"{file} was failed to upload its data to Database";
        }

        private async Task<IList<FullReport>> GetFullReportData(string file)
        {
            IList<FullReport> fullReports = new List<FullReport>();
            await Task.Run(() =>
            {
                string fileExtension = Path.GetExtension(file);

                using (var stream = new FileStream(file, FileMode.Open))
                {
                    stream.Position = 0;
                    ISheet sheet;
                    if (fileExtension == ".xls")
                    {
                        HSSFWorkbook workbook = new HSSFWorkbook(stream);
                        sheet = workbook.GetSheetAt(0);
                    }
                    else
                    {
                        XSSFWorkbook workbook = new XSSFWorkbook(stream);
                        sheet = workbook.GetSheetAt(0);
                    }

                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        FullReport fullReport = new FullReport();
                        IRow row = sheet.GetRow(i);
                        if (row == null)
                        {
                            continue;
                        }

                        if (row.Cells.All(d => d.CellType == CellType.Blank))
                        {
                            continue;
                        }

                        int.TryParse(row.GetCell(0)?.StringCellValue, out int tempInt);
                        fullReport.BookingNumber = tempInt;
                        fullReport.Status = row.GetCell(1)?.StringCellValue.ToUpper();
                        fullReport.SystemAgency = row.GetCell(3)?.StringCellValue;
                        fullReport.DatevAgencyAccount = row.GetCell(5)?.StringCellValue;
                        fullReport.Gds = row.GetCell(6)?.StringCellValue.ToUpper();
                        fullReport.PassengerNames = row.GetCell(7)?.StringCellValue.ToUpper();
                        int.TryParse(row.GetCell(8)?.StringCellValue, out tempInt);
                        fullReport.PassengerCount = tempInt;
                        fullReport.FirstAirline = row.GetCell(9)?.StringCellValue;
                        if (row.GetCell(10) != null)
                        {
                            string str = row.GetCell(10).StringCellValue;
                            if (str.Length > 5)
                            {
                                fullReport.Ticket = str;
                            }
                        }

                        fullReport.FirstGdsBookingNumber = row.GetCell(11)?.StringCellValue;
                        fullReport.FirstGdsBookingAlias = row.GetCell(12)?.StringCellValue;
                        string hour = row.GetCell(14).StringCellValue.Substring(0, 2);
                        string minutes = row.GetCell(14).StringCellValue.Substring(3, 2);
                        string day = row.GetCell(13).StringCellValue.Substring(0, 2);
                        string month = row.GetCell(13).StringCellValue.Substring(3, 2);
                        string year = row.GetCell(13).StringCellValue.Substring(6);
                        fullReport.BookingDate = new DateTime(year: int.Parse(year), month: int.Parse(month),
                            day: int.Parse(day), hour: int.Parse(hour), minute: int.Parse(minutes), second: 0);
                        fullReport.FirstRoute = row.GetCell(15).StringCellValue;

                        day = row.GetCell(16).StringCellValue.Substring(0, 2);
                        month = row.GetCell(16).StringCellValue.Substring(3, 2);
                        year = row.GetCell(16).StringCellValue.Substring(6);
                        fullReport.DepartureDate =
                            new DateTime(year: int.Parse(year), month: int.Parse(month), day: int.Parse(day));

                        if (row.GetCell(17) != null)
                        {
                            day = row.GetCell(17).StringCellValue.Substring(0, 2);
                            month = row.GetCell(17).StringCellValue.Substring(3, 2);
                            year = row.GetCell(17).StringCellValue.Substring(6);
                            fullReport.ReturnDate = new DateTime(year: int.Parse(year), month: int.Parse(month),
                                day: int.Parse(day));
                        }
                        else
                        {
                            fullReport.ReturnDate = new DateTime(1900, 1, 1);
                        }

                        if (row.GetCell(28) != null)
                        {
                            fullReport.Tariff = (decimal)row.GetCell(28).NumericCellValue;
                        }

                        if (row.GetCell(29) != null)
                        {
                            fullReport.Tax = (decimal)row.GetCell(29).NumericCellValue;
                        }

                        if (row.GetCell(30) != null)
                        {
                            fullReport.FullScFlex = (decimal)row.GetCell(30).NumericCellValue;
                        }

                        if (row.GetCell(31) != null)
                        {
                            fullReport.BloPartScFlex = (decimal)row.GetCell(31).NumericCellValue;
                        }

                        if (row.GetCell(32) != null)
                        {
                            fullReport.PartnerPartScFlex = (decimal)row.GetCell(32).NumericCellValue;
                        }

                        if (row.GetCell(33) != null)
                        {
                            fullReport.BloFixSc = (decimal)row.GetCell(33).NumericCellValue;
                        }

                        if (row.GetCell(34) != null)
                        {
                            fullReport.PartnerFixSc = (decimal)row.GetCell(34).NumericCellValue;
                        }

                        if (row.GetCell(35) != null)
                        {
                            fullReport.TotalPrice = (decimal)row.GetCell(35).NumericCellValue;
                        }

                        fullReport.SellingCurrency = row.GetCell(26)?.StringCellValue;
                        if (row.GetCell(27) != null)
                        {
                            fullReport.ExchangeRateToEuro = (decimal)row.GetCell(27).NumericCellValue;
                        }

                        fullReport.PaymentMethod = row.GetCell(36)?.StringCellValue;
                        fullReport.SalesPoint = row.GetCell(37)?.StringCellValue;
                        fullReport.Agent = row.GetCell(38)?.StringCellValue.ToUpper();
                        fullReport.CardType = row.GetCell(39)?.StringCellValue.ToUpper();
                        fullReport.CardHolder = row.GetCell(40)?.StringCellValue.ToUpper();
                        fullReport.CustomerGender = row.GetCell(41)?.StringCellValue.ToUpper();
                        fullReport.CustomerFirstName = row.GetCell(42)?.StringCellValue.ToUpper();
                        fullReport.CustomerLastName = row.GetCell(43)?.StringCellValue.ToUpper();
                        fullReport.CustomerCountry = row.GetCell(44)?.StringCellValue.ToUpper();
                        fullReport.CustomerCity = row.GetCell(45)?.StringCellValue.ToUpper();
                        fullReport.CustomerAddress = row.GetCell(46)?.StringCellValue.ToUpper();
                        fullReport.CustomerEmail = row.GetCell(47)?.StringCellValue.ToLower();
                        fullReport.CustomerPhone = row.GetCell(48)?.StringCellValue;
                        int.TryParse(row.GetCell(50)?.StringCellValue, out tempInt);
                        fullReport.NumberOfSegments = tempInt;
                        fullReport.ClearingType = row.GetCell(66)?.StringCellValue.ToUpper();
                        fullReport.BookingClass = row.GetCell(67)?.StringCellValue.ToUpper();
                        fullReport.FareBasis = row.GetCell(68)?.StringCellValue.ToUpper();
                        fullReport.Commission = row.GetCell(69)?.ToString();

                        fullReports.Add(fullReport);
                    }
                }
            });
            return fullReports;
        }

        public async Task<IList<string>> OnPostFileReportImport(IList<string> files, string connectionString, Dictionary<string, object> optionalParams)
        {
            IList<string> resultMessages = new List<string>();
            foreach (var formFile in files)
            {
                var message = await OnPostFileReportImport(formFile, connectionString, optionalParams);
                resultMessages.Add(message);
            }

            return resultMessages;
        }

        private async Task<bool> BulkCopyToDatabase<T>(IList<T> dataList, string connectionString, Dictionary<string, object> optionalParams)
        {
            return await Task.Run(() => BulkCopy(dataList, connectionString, optionalParams), CancellationToken.None);
        }

        private bool BulkCopy<T>(IList<T> dataList, string connectionString, Dictionary<string, object> optionalParams)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (var sbCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        sbCopy.BulkCopyTimeout = 0;
                        sbCopy.BatchSize = 100;
                        sbCopy.DestinationTableName = optionalParams["Db_Table"] as string;//"FullReports";

                        var reader = dataList.AsDataTable();
                        if (optionalParams["ColumnsToRemove"] is string[] list)
                        {
                            foreach (var columns in list)
                            {
                                reader.Columns.Remove(columns); //"CustomerFullName");
                            }
                        }
                        sbCopy.WriteToServer(reader);
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
                finally
                {
                    transaction.Dispose();
                    connection.Close();
                }
            }

            return true;
        }
    }
}
