using System;
using System.ComponentModel.DataAnnotations;

namespace BERlogic.CallCenter.Models
{
    public class FileInFolder
    {
        [Display(Name = "FileInFolder.FilePath.DisplayName")]
        public string FilePath { get; set; }
        [Display(Name = "FileInFolder.FileName.DisplayName")]
        public string FileName { get; set; }
        public bool IsSuccess { get; set; }
        [Display(Name = "FileInFolder.DateTimeOfUpload.DisplayName")]
        public DateTimeOffset DateTimeOfUpload { get; set; }
        [Display(Name = "FileInFolder.FileSize.DisplayName")]
        public long FileSize { get; set; }
        [Display(Name = "FileInFolder.FileExtension.DisplayName")]
        public string FileExtension { get; set; }
    }
}
