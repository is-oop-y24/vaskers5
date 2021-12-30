using System.Collections.Generic;
using System.IO;

namespace Backups.Entities;

public class Repository
{
    public Repository()
    {
        RepoFiles = new List<FileInfo>();
    }

    public List<FileInfo> RepoFiles { get; }

    public Repository AddFiles(List<FileInfo> files)
    {
        RepoFiles.AddRange(files);
        return this;
    }
}