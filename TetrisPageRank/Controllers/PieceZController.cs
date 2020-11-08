using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceZController : IPieceController
    {
        public IEnumerable<int> GetPossibleStacks(int stack)
        {
            var digits = TetrisStack.GetReadableDigits(stack);

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                yield return Drop0(stack, i);
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop90Possible(digits, i))
                {
                    continue;
                }

                yield return Drop90(stack, i);
            }
        }

        public IEnumerable<TetrisDrop> GetPossibleDrops(int stack)
        {
            var digits = TetrisStack.GetReadableDigits(stack);

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                yield return new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.Z);
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop90Possible(digits, i))
                {
                    continue;
                }

                int newStack = Drop90(stack, i);
                yield return new TetrisDrop(newStack, Orientation.DEGREES_90, i, Piece.Z);
            }
        }

        private int Drop0(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +1);
            }

            if (column != 6)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 2, -1);
            }

            stack = TetrisStack.SetColumnHeight(stack, column, 0);
            return TetrisStack.SetColumnHeight(stack, column + 1, -1);
        }

        private int Drop90(int stack, int column)
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
            return readableDigits[column] == -1 && readableDigits[column + 1] == 0;
        }

        private bool IsDrop90Possible(int[] readableDigits, int column)
        {
            return readableDigits[column] == 1;
        }
    }
}
