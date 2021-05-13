using System;
using System.Collections.Generic;
using TetrisPageRank.Models;

namespace TetrisPageRank.Controllers
{
    public class PieceJController : IPieceController
    {
        public float GetBestPossibleRank(int stack)
        {
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
            
            float bestRank = 0;

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                int newStack = Drop0(stack, i);
                float rank = Ranks.Current[Ranks.Indexes[newStack]];

                if (rank > bestRank)
                {
                    bestRank = rank;
                }
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop90Possible(digits, i))
                {
                    continue;
                }

                int newStack = Drop90(stack, i);
                float rank = Ranks.Current[Ranks.Indexes[newStack]];

                if (rank > bestRank)
                {
                    bestRank = rank;
                }
            }

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop180Possible(digits, i))
                {
                    continue;
                }

                int newStack = Drop180(stack, i);
                float rank = Ranks.Current[Ranks.Indexes[newStack]];

                if (rank > bestRank)
                {
                    bestRank = rank;
                }
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop270Possible(digits, i))
                {
                    continue;
                }

                int newStack = Drop270(stack, i);
                float rank = Ranks.Current[Ranks.Indexes[newStack]];

                if (rank > bestRank)
                {
                    bestRank = rank;
                }
            }

            return bestRank;
        }

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

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop180Possible(digits, i))
                {
                    continue;
                }

                possibleStacks.Add(Drop180(stack, i));
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop270Possible(digits, i))
                {
                    continue;
                }

                possibleStacks.Add(Drop270(stack, i));
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

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop0Possible(digits, i))
                {
                    continue;
                }

                possibleDrops.Add(new TetrisDrop(Drop0(stack, i), Orientation.DEGREES_0, i, Piece.J));
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop90Possible(digits, i))
                {
                    continue;
                }

                possibleDrops.Add(new TetrisDrop(Drop90(stack, i), Orientation.DEGREES_90, i, Piece.J));
            }

            for (int i = 0; i <= 6; i++)
            {
                if (!IsDrop180Possible(digits, i))
                {
                    continue;
                }

                possibleDrops.Add(new TetrisDrop(Drop180(stack, i), Orientation.DEGREES_180, i, Piece.J));
            }

            for (int i = 0; i <= 7; i++)
            {
                if (!IsDrop270Possible(digits, i))
                {
                    continue;
                }

                possibleDrops.Add(new TetrisDrop(Drop270(stack, i), Orientation.DEGREES_270, i, Piece.J));
            }

            return possibleDrops;
        }

        private int Drop0(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +2);
            }

            if (column != 6)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 2, -1);
            }

            return TetrisStack.SetColumnHeight(stack, column, -1);
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

            return TetrisStack.SetColumnHeight(stack, column, 2);
        }

        private int Drop180(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column - 1, +1);
            }

            if (column != 6)
            {
                stack = TetrisStack.ModifyColumnHeight(stack, column + 2, -2);
            }

            return TetrisStack.SetColumnHeight(stack, column + 1, 0);
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

            return TetrisStack.SetColumnHeight(stack, column, 0);
        }

        private bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == 0;
        }

        private bool IsDrop90Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0;
        }

        private bool IsDrop180Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == -1;
        }

        private bool IsDrop270Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 2;
        }
    }
}
