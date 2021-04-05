using System;
using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceZController : IPieceController
    {
        public List<int> GetPossibleStacks(int stack)
        {
            var possibleStacks = new List<int>();

            ReadOnlySpan<int> digits = stackalloc[]
            {
                TetrisStack.GetReadableDigit(stack, 0),
                TetrisStack.GetReadableDigit(stack, 1),
                TetrisStack.GetReadableDigit(stack, 2),
                TetrisStack.GetReadableDigit(stack, 3),
                TetrisStack.GetReadableDigit(stack, 4),
                TetrisStack.GetReadableDigit(stack, 5),
                TetrisStack.GetReadableDigit(stack, 6),
                TetrisStack.GetReadableDigit(stack, 7)
            };

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                possibleStacks.Add(Drop0(stack, i));
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop90Possible(digits, i))
                {
                    continue;
                }

                possibleStacks.Add(Drop90(stack, i));
            }

            return possibleStacks;
        }

        public List<TetrisDrop> GetPossibleDrops(int stack)
        {
            var possibleStacks = new List<TetrisDrop>();

            ReadOnlySpan<int> digits = stackalloc[]
            {
                TetrisStack.GetReadableDigit(stack, 0),
                TetrisStack.GetReadableDigit(stack, 1),
                TetrisStack.GetReadableDigit(stack, 2),
                TetrisStack.GetReadableDigit(stack, 3),
                TetrisStack.GetReadableDigit(stack, 4),
                TetrisStack.GetReadableDigit(stack, 5),
                TetrisStack.GetReadableDigit(stack, 6),
                TetrisStack.GetReadableDigit(stack, 7)
            };

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                possibleStacks.Add(new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.Z));
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop90Possible(digits, i))
                {
                    continue;
                }

                int newStack = Drop90(stack, i);
                possibleStacks.Add(new TetrisDrop(newStack, Orientation.DEGREES_90, i, Piece.Z));
            }

            return possibleStacks;
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

        private bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == -1 && readableDigits[column + 1] == 0;
        }

        private bool IsDrop90Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 1;
        }
    }
}
