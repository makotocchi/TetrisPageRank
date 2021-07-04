using System.Collections.Generic;

namespace TetrisPageRank.Services
{
    public abstract class PieceService
    {
        public abstract List<TetrisDrop> GetPossibleDrops(int[] columns);
        public abstract float GetBestPossibleRank(int stack);

        protected static float CalculateBestRank(int newStack, float currentBestRank)
        {
            float rank = Ranks.GetCurrentRank(newStack);

            if (rank > currentBestRank)
            {
                return rank;
            }

            return currentBestRank;
        }
    }
}
