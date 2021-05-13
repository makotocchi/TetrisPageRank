using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public interface IPieceController
    {
        List<TetrisDrop> GetPossibleDrops(int stack);
        List<int> GetPossibleStacks(int stack);
        float GetBestPossibleRank(int stack);
    }
}
