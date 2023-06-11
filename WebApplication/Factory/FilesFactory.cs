using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Factory
{
    public class FilesFactory
    {
        public static string _oldPath;
        public static List<FileModel> _files;

        public static List<FileModel> LoadFiles(string fullPathDirectory)
        {
            if (!Directory.Exists(fullPathDirectory))                                               // Check if directory exist
            {                                                                                       // If directory does not exist, it sets default values in variables and finished
                _oldPath = null;
                _files = new List<FileModel>();
                return null;
            }

            var newFilePaths = Directory.GetFiles(fullPathDirectory, "*", SearchOption.AllDirectories).ToList();
            var newFiles = newFilePaths.Select(item => new FileModel            // If directory exist load file models
            {
                Name = Path.GetFileName(item),
                Path = item,
                ShortPath = item.Substring(fullPathDirectory.Length + 1),
                Modified = File.GetLastWriteTime(item)
            }).ToList();
            
            return newFiles;
        }

        public static bool IsNewDirectory(string fullPathDirectory)                 
        {
            return string.IsNullOrEmpty(_oldPath) || _oldPath != fullPathDirectory;                 // It check if it is new directory or it check the same directory
        }

        public static DirectoryResultModel ChangesInDirectory(List<FileModel> newFiles)             // It compares old files with new files 
        {
            var result = new DirectoryResultModel();
            foreach (var newFileModel in newFiles)
            {
                if (IsAddedFile(newFileModel, result))                                              
                    continue;

                IsModifiedFile(newFileModel, result);
            }
            CheckDeletedFiles(newFiles, result);
            return result;
        }

        public static string ViewResults(DirectoryResultModel result)                               // It prepares result text
        {
            string textResult;
            if (result.AddedFiles.Any() || result.ModifiedFiles.Any() || result.DeletedFiles.Any())
                textResult = string.Join("<br>", new List<string>
                    {
                        string.Join("<br>", result.AddedFiles.Select(f => $"[A] {f.ShortPath}")),
                        string.Join("<br>", result.ModifiedFiles.Select(f => $"[M] {f.ShortPath} verze({f.Version})")),
                        string.Join("<br>", result.DeletedFiles.Select(f => $"[D] {f.ShortPath}"))
                    }.Where(j => !string.IsNullOrEmpty(j))
                );
            else
                textResult = "žádna změna";
            
            return textResult;
        }

        private static void CheckDeletedFiles(List<FileModel> newFiles, DirectoryResultModel result)
        {
            result.DeletedFiles = _files.Where(f => !newFiles.Any(n => n.Path == f.Path)).Select(s => new FileModel
            {
                Path = s.Path,
                ShortPath = s.ShortPath,
                Name = s.Name,
                Modified = s.Modified,
                Version = s.Version
            }).ToList();
            _files.RemoveAll(f => newFiles.All(n => n.Path != f.Path));
        }

        private static void IsModifiedFile(FileModel newFileModel, DirectoryResultModel result)
        {
            var oldFileModified = _files.FirstOrDefault(f => f.Path == newFileModel.Path && f.Modified < newFileModel.Modified);
            if (oldFileModified != null)
            {
                oldFileModified.Modified = newFileModel.Modified;
                oldFileModified.Version++;
                result.ModifiedFiles.Add(oldFileModified);
            }
        }

        private static bool IsAddedFile(FileModel newFileModel, DirectoryResultModel result)
        {
            if (_files.All(f => f.Path != newFileModel.Path))
            {
                result.AddedFiles.Add(newFileModel);
                _files.Add(newFileModel);
                return true;
            }

            return false;
        }
    }
}