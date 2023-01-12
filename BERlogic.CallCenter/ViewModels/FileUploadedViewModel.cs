using System.Collections.Generic;

namespace BERlogic.CallCenter.ViewModels
{
    public class FileUploadedViewModel
    {
        public List<string> FilesUploaded { get; set; }
        public List<string> Messages { get; set; } = new List<string>();

        public FileUploadedViewModel()
        {
            FilesUploaded = new List<string>();
        }
    }
}
