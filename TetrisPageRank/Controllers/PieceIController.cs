using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceIController : IPieceController
    {
        public IEnumerable<int> GetPossibleStacks(int stack)
        {
            var digits = TetrisStack.GetReadableDigits(stack);

            for (int i = 0; i <= 5; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                yield return Drop0(stack, i);
            }

            for (int i = 0; i <= 8; i++)
            {
                yield return Drop90(stack, i);
            }
        }

        public IEnumerable<TetrisDrop> GetPossibleDrops(int stack)
        {
            var digits = TetrisStack.GetReadableDigits(stack);

            for (int i = 0; i <= 5; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                yield return new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.I);
            }

            for (int i = 0; i <= 8; i++)
            {
                yield return new TetrisDrop(Drop90(stack, i), Orientation.DEGREES_90, i, Piece.I);
            }
        }

        private int Drop0(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +1);
            }

            if (column != 5)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 3, -1);
            }

            return stack;
        }

        private int Drop90(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +4);
            }

            if (column != 8)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column, -4);
            }

            return stack;
        }

        private bool IsDrop0Possible(int[] readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == 0 && readableDigits[column + 2] == 0;
        }
    }
}