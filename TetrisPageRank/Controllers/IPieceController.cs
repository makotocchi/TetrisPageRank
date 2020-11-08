using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public interface IPieceController
    {
        IEnumerable<TetrisDrop> GetPossibleDrops(int stack);
        IEnumerable<int> GetPossibleStacks(int stack);
    }
}
