using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BERlogic.CallCenter.Data;
using BERlogic.CallCenter.Models.Repositories;
using BERlogic.CallCenter.Services;
using BERlogic.CallCenter.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace BERlogic.CallCenter.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IFileReportJournal _fullReportJournal;
        private readonly IFullReport _fullReport;
        private readonly IStringLocalizer<FileUploadController> _localizer;


        public FileUploadController(IFileReportJournal fileReportJournal, IFullReport report, IStringLocalizer<FileUploadController> localizer)
        {
            _fullReportJournal = fileReportJournal;
            _fullReport = report;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult UploadFile(FileUploadedViewModel model = null)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            FileUploadedViewModel model = new FileUploadedViewModel();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "fullreport");
            List<string> uploadedFiles = new List<string>();

            foreach (var file in files)
            {
                if (file.FileName.ToLower().Contains("fullreport") && file.FileName.Contains("xlsx"))
                {
                    var folderFiles = Directory.GetFiles(path).ToList().Count;
                    var uploadFilePath = Path.Combine(path,
                        $"fullreport_{DateTime.Now:ddMMyyyyHHmm}_{folderFiles + 1}.xlsx");
                    using (var stream = new FileStream(uploadFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.FilesUploaded.Add(file.FileName);
                        uploadedFiles.Add(uploadFilePath);
                        model.Messages.Add($"{file.FileName} {_localizer["was successfuly uploaded to the Server."]}");
                    }
                }
                else
                {
                    model.Messages.Add($"{file.FileName} {_localizer["is failed to upload to the Server."]}");
                }
            }

            Dictionary<string, object> options = new Dictionary<string, object>
            {
                ["UploadFolder"] = "fullreport",
                ["SuccessFolder"] = "success",
                ["Model"] = "FullReport",
                ["Db_Table"] = "FullReports",
                ["ColumnsToRemove"] = new[] { "CustomerFullName" },
                ["Journal"] = _fullReportJournal,
                ["Repository"] = _fullReport
            };

            IExcelParse parse = new ExcelParse();
            var dbCore = HttpContext.RequestServices.GetService<ApplicationDbProcessContext>();
            var messages = await parse.OnPostFileReportImport(uploadedFiles, dbCore.Database.GetDbConnection().ConnectionString, options);
            model.Messages = (List<string>)messages;

            return View(nameof(UploadFile), model);
        }

        public IActionResult DeleteFile(string path = null)
        {
            System.IO.File.Delete(path);
            return RedirectToAction("UploadFile");
        }

        public async Task<IActionResult> UploadFileToDatabaseAsync(string path = null)
        {
            FileUploadedViewModel model = new FileUploadedViewModel();
            Dictionary<string, object> options = new Dictionary<string, object>
            {
                ["UploadFolder"] = "fullreport",
                ["SuccessFolder"] = "success",
                ["Model"] = "FullReport",
                ["Db_Table"] = "FullReports",
                ["ColumnsToRemove"] = new[] { "CustomerFullName" },
                ["Journal"] = _fullReportJournal,
                ["Repository"] = _fullReport
            };

            IExcelParse parse = new ExcelParse();
            var dbCore = HttpContext.RequestServices.GetService<ApplicationDbProcessContext>();
            var messages = await parse.OnPostFileReportImport(path, dbCore.Database.GetDbConnection().ConnectionString, options);
            model.Messages.Add(messages);

            return RedirectToAction(nameof(UploadFile), model);
        }
    }
}