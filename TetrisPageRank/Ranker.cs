using Serilog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TetrisPageRank
{
    public static class Ranker
    {
        public static void Iterate(int n)
        {
            Log.Information("Iterating {n} times", n);

            var stopwatch = new Stopwatch();

            for (int i = 1; i <= n; i++)
            {
                Log.Information("Iteration {i}", i);
                stopwatch.Restart();

                Parallel.ForEach(Ranks.Indexes, index =>
                {
                    Ranks.SetNextRank(index.Key, CalculateRank(index.Key));
                });

                Ranks.UpdateCurrentRankList();

                stopwatch.Stop();
                Log.Information("Time elapsed: {0}", stopwatch.Elapsed);
            }
        }

        private static float CalculateRank(int stack)
        {
            var totalRank = 0f;

            foreach (var piece in Piece.All)
            {
                totalRank += piece.Service.GetBestPossibleRank(stack);
            }

            return totalRank / Piece.All.Length;
        }
    }
}
