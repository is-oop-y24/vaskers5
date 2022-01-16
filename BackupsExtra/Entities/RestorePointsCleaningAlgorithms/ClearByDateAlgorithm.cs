using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;

namespace BackupsExtra.Entities.RestorePointsCleaningAlgorithms
{
    public class ClearByDateAlgorithm : IClearAlgorithm
    {
        public ClearByDateAlgorithm(DateTime limit)
        {
            Limit = limit;
        }

        public DateTime Limit { get; }

        public List<RestorePoint> MakeClear(List<RestorePoint> points)
        {
            var invalidPoints = points.Where(point => point.CreationTime < Limit).ToList();
            return invalidPoints;
        }
    }
}