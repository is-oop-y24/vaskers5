using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint(int id, string restorePointPath, List<ZipArchive> archives)
        {
            Id = id;
            RestorePointPath = restorePointPath;
            Archives = archives;
            CreationTime = DateTime.Now;
        }

        public RestorePoint(int id, string restorePointPath, ZipArchive archive)
            : this(id, restorePointPath, new List<ZipArchive>() { archive })
        {
        }

        public virtual DateTime CreationTime { get; set; }

        public virtual List<ZipArchive> Archives { get; set; }

        public virtual int Id { get; set; }
        public string RestorePointPath { get; }
    }
}