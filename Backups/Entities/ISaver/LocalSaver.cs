using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Entities.ISaver;

public class LocalSaver : ISaver
{
    public void Save(RestorePoint point, List<Repository> repos)
    {
        int counter = 0;
        if (!Directory.Exists(point.RestorePointPath))
        {
            Directory.CreateDirectory(point.RestorePointPath);
        }

        foreach (var repo in repos)
        {
            var tempDir = new DirectoryInfo(Path.Combine(point.RestorePointPath, "temp"));
            tempDir.Create();
            foreach (var file in repo.RepoFiles)
            {
                file.CopyTo(Path.Combine(tempDir.FullName, file.Name));
            }

            ZipFile.CreateFromDirectory(tempDir.FullName, Path.Combine(point.RestorePointPath, $"{counter++}.zip"));
            tempDir.Delete(true);
        }

        point.Archives.AddRange(repos);
    }
}