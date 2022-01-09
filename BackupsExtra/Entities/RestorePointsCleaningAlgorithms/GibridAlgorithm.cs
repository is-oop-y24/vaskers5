using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;

namespace BackupsExtra.Entities.RestorePointsCleaningAlgorithms
{
    public class GibridAlgorithm : IClearAlgorithm
    {
        public GibridAlgorithm(List<IClearAlgorithm> algorithms)
        {
            Algorithms = new List<IClearAlgorithm>();
        }

        public List<IClearAlgorithm> Algorithms { get; }

        public List<RestorePoint> MakeClear(List<RestorePoint> points)
        {
            return Algorithms.Aggregate(points, (current, algo) => algo.MakeClear(current));
        }
    }
}