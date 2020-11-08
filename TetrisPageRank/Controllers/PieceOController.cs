using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceOController : IPieceController
    {
        public IEnumerable<int> GetPossibleStacks(int stack)
        {
            var digits = TetrisStack.GetReadableDigits(stack);

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                yield return Drop0(stack, i);
            }
        }

        public IEnumerable<TetrisDrop> GetPossibleDrops(int stack)
        {
            var digits = TetrisStack.GetReadableDigits(stack);

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                yield return new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.O);
            }
        }

        private int Drop0(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +2);
            }

            if (column != 7)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 1, -2);
            }

            return stack;
        }

        private bool IsDrop0Possible(int[] readableDigits, int column)
        {
            return readableDigits[column] == 0;
        }
    }
}
