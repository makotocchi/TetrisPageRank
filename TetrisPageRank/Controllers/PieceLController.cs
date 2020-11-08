using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceLController : IPieceController
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

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop180Possible(digits, i))
                {
                    continue;
                }

                yield return Drop180(stack, i);
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop270Possible(digits, i))
                {
                    continue;
                }

                yield return Drop270(stack, i);
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

                yield return new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.L);
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop90Possible(digits, i))
                {
                    continue;
                }

                yield return new TetrisDrop(Drop90(stack, i), Orientation.DEGREES_90, i, Piece.L);
            }

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop180Possible(digits, i))
                {
                    continue;
                }

                yield return new TetrisDrop(Drop180(stack, i), Orientation.DEGREES_180, i, Piece.L);
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop270Possible(digits, i))
                {
                    continue;
                }

                yield return new TetrisDrop(Drop270(stack, i), Orientation.DEGREES_270, i, Piece.L);
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
                stack = TetrisStack.ModifyColumnHeight(stack, column + 2, -2);
            }

            return TetrisStack.SetColumnHeight(stack, column + 1, 1);
        }

        private int Drop90(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +1);
            }

            if (column != 7)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 1, -3);
            }

            return TetrisStack.SetColumnHeight(stack, column, 0);
        }

        private int Drop180(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +2);
            }

            if (column != 6)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 2, -1);
            }

            return TetrisStack.SetColumnHeight(stack, column, 0);
        }

        private int Drop270(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +3);
            }

            if (column != 7)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 1, -1);
            }

            return TetrisStack.SetColumnHeight(stack, column, -2);
        }

        private bool IsDrop0Possible(int[] readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == 0;
        }

        private bool IsDrop90Possible(int[] readableDigits, int column)
        {
            return readableDigits[column] == -2;
        }

        private bool IsDrop180Possible(int[] readableDigits, int column)
        {
            return readableDigits[column] == 1 && readableDigits[column + 1] == 0;
        }

        private bool IsDrop270Possible(int[] readableDigits, int column)
        {
            return readableDigits[column] == 0;
        }
    }
}
