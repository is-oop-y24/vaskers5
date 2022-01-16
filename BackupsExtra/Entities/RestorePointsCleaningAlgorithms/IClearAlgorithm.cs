using System.Collections.Generic;
using Backups.Entities;

namespace BackupsExtra.Entities.RestorePointsCleaningAlgorithms
{
    public interface IClearAlgorithm
    {
        List<RestorePoint> MakeClear(List<RestorePoint> points);
    }
}