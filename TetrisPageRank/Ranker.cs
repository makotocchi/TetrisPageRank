using Serilog;
using System.Threading.Tasks;

namespace TetrisPageRank
{
    public class Ranker
    {
        public static void Iterate(int n)
        {
            Log.Information("Iterating {n} times", n);

            for (int i = 1; i <= n; i++)
            {
                Log.Information("Iteration {i}", i);

                Parallel.ForEach(Ranks.Indexes, index =>
                {
                    Ranks.SetNextRank(index.Key, CalculateRank(index.Key));
                });

                Ranks.UpdateCurrentRankList();
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
