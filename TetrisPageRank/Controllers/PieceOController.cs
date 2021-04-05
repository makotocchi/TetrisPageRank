using System;
using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceOController : IPieceController
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

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                possibleStacks.Add(Drop0(stack, i));
            }

            return possibleStacks;
        }

        public List<TetrisDrop> GetPossibleDrops(int stack)
        {
            var possibleDrops = new List<TetrisDrop>();

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

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                possibleDrops.Add(new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.O));
            }

            return possibleDrops;
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

        private bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0;
        }
    }
}
