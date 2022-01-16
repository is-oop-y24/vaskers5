using System.Collections.Generic;
using System.Linq;
using Backups.Entities;

namespace BackupsExtra.Entities.RestorePointsCleaningAlgorithms
{
    public class ClearByNumberAlgorithm : IClearAlgorithm
    {
        public ClearByNumberAlgorithm(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; }

        public List<RestorePoint> MakeClear(List<RestorePoint> points)
        {
            var invalidPoints = points.Count > Limit ? points.Take(points.Count - Limit).ToList() : new List<RestorePoint>();
            return invalidPoints;
        }
    }
}