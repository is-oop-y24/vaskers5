using System.Collections.Generic;
using System.IO;

namespace Backups.Entities.ISaver;

public interface ISaver
{
    void Save(RestorePoint point, List<Repository> repository);
}