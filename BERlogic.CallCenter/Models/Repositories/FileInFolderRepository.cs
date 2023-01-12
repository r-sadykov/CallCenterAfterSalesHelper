using System.Collections.Generic;
using System.IO;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class FileInFolderRepository : IFileInFolder
    {
        private string _path;
        private string _fullReportPath;
        private string _fullReportSuccessPath;

        public FileInFolderRepository()
        {
            _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            _fullReportPath = Path.Combine(_path, "fullreport");
            if (!Directory.Exists(_fullReportPath))
            {
                Directory.CreateDirectory(_fullReportPath);
            }
            _fullReportSuccessPath = Path.Combine(_fullReportPath, "success");
            if (!Directory.Exists(_fullReportSuccessPath))
            {
                Directory.CreateDirectory(_fullReportSuccessPath);
            }
        }

        public IList<FileInFolder> FilesInFolder
        {
            get
            {
                IList<FileInFolder> list = new List<FileInFolder>();
                if (Directory.Exists(_fullReportPath))
                {
                    var files = Directory.GetFiles(_fullReportPath);
                    foreach (var file in files)
                    {
                        FileInFolder fileInFolder = new FileInFolder
                        {
                            FilePath = file,
                            FileName = Path.GetFileNameWithoutExtension(file),
                            FileExtension = Path.GetExtension(file),
                            DateTimeOfUpload = File.GetLastWriteTime(file),
                            FileSize = new System.IO.FileInfo(file).Length,
                            IsSuccess = false
                        };
                        list.Add(fileInFolder);
                    }
                }

                return list;
            }
        }

        public IList<FileInFolder> FilesInSuccessFolder
        {
            get
            {
                IList<FileInFolder> list = new List<FileInFolder>();
                if (Directory.Exists(_fullReportSuccessPath))
                {
                    var files = Directory.GetFiles(_fullReportSuccessPath);
                    foreach (var file in files)
                    {
                        FileInFolder fileInFolder = new FileInFolder
                        {
                            FilePath = file,
                            FileName = Path.GetFileNameWithoutExtension(file),
                            FileExtension = Path.GetExtension(file),
                            DateTimeOfUpload = File.GetLastWriteTime(file),
                            FileSize = new System.IO.FileInfo(file).Length,
                            IsSuccess = true
                        };
                        list.Add(fileInFolder);
                    }
                }

                return list;
            }
        }
    }
}
