using System;
using System.IO;

namespace Backups.Entities
{
    public class SystemFile
    {
        public SystemFile(string path, DateTime time)
        {
            JustFile = new FileInfo(path);
            FileTime = time;
        }

        public DateTime FileTime { get; set; }

        public FileInfo JustFile { get; set; }
    }
}