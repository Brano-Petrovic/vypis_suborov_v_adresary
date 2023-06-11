using System;

namespace WebApplication.Models
{
    public class FileModel
    {
        public FileModel()
        {
            Version = 1;
        }
        public string Path { get; set; }
        public string ShortPath { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime Modified { get; set; }
    }

}