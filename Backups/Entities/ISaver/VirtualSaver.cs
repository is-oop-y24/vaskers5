using System.Collections.Generic;

namespace Backups.Entities.ISaver;

public class VirtualSaver : ISaver
{
    public void Save(RestorePoint point, List<Repository> repository)
    {
        point.Archives.AddRange(repository);
    }
}