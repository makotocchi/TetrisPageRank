using System;
using System.Collections.Generic;

namespace TetrisPageRank.Services
{
    public class PieceSService : PieceService
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
                    possibleDrops.Add(new TetrisDrop(Drop0(stack, i), 0, i, Piece.S));
                }
            }

            for (int i = 0; i <= 7; i++)
            {
                if (IsDrop90Possible(digits, i))
                {
                    possibleDrops.Add(new TetrisDrop(Drop90(stack, i), 90, i, Piece.S));
                }
            }

            return possibleDrops;
        }

        private static int Drop0(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +1);
            }

            if (column != 6)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 2, -1);
            }

            stack = TetrisStackHelper.SetColumnHeight(stack, column, 1);
            return TetrisStackHelper.SetColumnHeight(stack, column + 1, 0);
        }

        private static int Drop90(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +2);
            }

            if (column != 7)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 1, -2);
            }

            return stack;
        }

        private static bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == 1;
        }

        private static bool IsDrop90Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == -1;
        }
    }
}
