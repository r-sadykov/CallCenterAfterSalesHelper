using System.Collections.Generic;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IFileInFolder
    {
        IList<FileInFolder> FilesInFolder { get; }
        IList<FileInFolder> FilesInSuccessFolder { get; }
    }
}
