using System.Collections.Generic;

namespace WebApplication.Models
{
    public class DirectoryResultModel
    {
        public DirectoryResultModel ()
        {
            AddedFiles = new List<FileModel>();
            ModifiedFiles = new List<FileModel>();
            DeletedFiles = new List<FileModel>();
        }

        public List<FileModel> AddedFiles { get; set; }
        public List<FileModel> ModifiedFiles { get; set; }
        public List<FileModel> DeletedFiles { get; set; }
    }

}