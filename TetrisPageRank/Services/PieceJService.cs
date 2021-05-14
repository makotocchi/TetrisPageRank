using System;
using System.Collections.Generic;

namespace TetrisPageRank.Services
{
    public class PieceJService : PieceService
    {
        public override float GetBestPossibleRank(int stack)
        {
            ReadOnlySpan<int> digits = stackalloc[]
            {
                TetrisStackHelper.GetReadableDigit(stack, 0),
                TetrisStackHelper.GetReadableDigit(stack, 1),
                TetrisStackHelper.GetReadableDigit(stack, 2),
                TetrisStackHelper.GetReadableDigit(stack, 3),
                TetrisStackHelper.GetReadableDigit(stack, 4),
                TetrisStackHelper.GetReadableDigit(stack, 5),
                TetrisStackHelper.GetReadableDigit(stack, 6),
                TetrisStackHelper.GetReadableDigit(stack, 7)
            };
            
            float bestRank = 0;

            for (int i = 0; i <= 6; i++)
            {
                if (IsDrop0Possible(digits, i))
                {
                    bestRank = CalculateBestRank(Drop0(stack, i), bestRank);
                }
            }

            for (int i = 0; i <= 7; i++)
            {
                if (IsDrop90Possible(digits, i))
                {
                    bestRank = CalculateBestRank(Drop90(stack, i), bestRank);
                }
            }

            for (int i = 0; i <= 6; i++)
            {
                if (IsDrop180Possible(digits, i))
                {
                    bestRank = CalculateBestRank(Drop180(stack, i), bestRank);
                }
            }

            for (int i = 0; i <= 7; i++)
            {
                if (IsDrop270Possible(digits, i))
                {
                    bestRank = CalculateBestRank(Drop270(stack, i), bestRank);
                }
            }

            return bestRank;
        }

        public override List<TetrisDrop> GetPossibleDrops(int stack)
        {
            var possibleDrops = new List<TetrisDrop>();

            ReadOnlySpan<int> digits = stackalloc[]
            {
                TetrisStackHelper.GetReadableDigit(stack, 0),
                TetrisStackHelper.GetReadableDigit(stack, 1),
                TetrisStackHelper.GetReadableDigit(stack, 2),
                TetrisStackHelper.GetReadableDigit(stack, 3),
                TetrisStackHelper.GetReadableDigit(stack, 4),
                TetrisStackHelper.GetReadableDigit(stack, 5),
                TetrisStackHelper.GetReadableDigit(stack, 6),
                TetrisStackHelper.GetReadableDigit(stack, 7)
            };

            for (int i = 0; i <= 6; i++)
            {
                if (IsDrop0Possible(digits, i))
                {
                    possibleDrops.Add(new TetrisDrop(Drop0(stack, i), 0, i, Piece.J));
                }
            }

            for (int i = 0; i <= 7; i++)
            {
                if (IsDrop90Possible(digits, i))
                {
                    possibleDrops.Add(new TetrisDrop(Drop90(stack, i), 90, i, Piece.J));
                }
            }

            for (int i = 0; i <= 6; i++)
            {
                if (IsDrop180Possible(digits, i))
                {
                    possibleDrops.Add(new TetrisDrop(Drop180(stack, i), 180, i, Piece.J));
                }
            }

            for (int i = 0; i <= 7; i++)
            {
                if (IsDrop270Possible(digits, i))
                {
                    possibleDrops.Add(new TetrisDrop(Drop270(stack, i), 270, i, Piece.J));
                }
            }

            return possibleDrops;
        }

        private static int Drop0(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +2);
            }

            if (column != 6)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 2, -1);
            }

            return TetrisStackHelper.SetColumnHeight(stack, column, -1);
        }

        private static int Drop90(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +1);
            }

            if (column != 7)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 1, -3);
            }

            return TetrisStackHelper.SetColumnHeight(stack, column, 2);
        }

        private static int Drop180(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +1);
            }

            if (column != 6)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 2, -2);
            }

            return TetrisStackHelper.SetColumnHeight(stack, column + 1, 0);
        }

        private static int Drop270(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +3);
            }

            if (column != 7)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 1, -1);
            }

            return TetrisStackHelper.SetColumnHeight(stack, column, 0);
        }

        private static bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == 0;
        }

        private static bool IsDrop90Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0;
        }

        private static bool IsDrop180Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == -1;
        }

        private static bool IsDrop270Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 2;
        }
    }
}
