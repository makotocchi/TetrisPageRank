using System;
using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceIController : IPieceController
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

            for (int i = 0; i <= 5; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                possibleStacks.Add(Drop0(stack, i));
            }

            for (int i = 0; i <= 8; i++)
            {
                possibleStacks.Add(Drop90(stack, i));
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

            for (int i = 0; i <= 5; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                possibleDrops.Add(new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.I));
            }

            for (int i = 0; i <= 8; i++)
            {
                possibleDrops.Add(new TetrisDrop(Drop90(stack, i), Orientation.DEGREES_90, i, Piece.I));
            }

            return possibleDrops;
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

        private bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == 0 && readableDigits[column + 2] == 0;
        }
    }
}