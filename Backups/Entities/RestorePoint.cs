using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint(int id, string restorePointPath)
        {
            RestorePointId = id;
            RestorePointPath = restorePointPath;
            Archives = new List<Repository>();
            CreationTime = DateTime.Now;
        }

        public virtual DateTime CreationTime { get; }

        public virtual List<Repository> Archives { get;  }

        public virtual int RestorePointId { get; }
        public string RestorePointPath { get; }
    }
}