using System;
using System.Collections.Generic;

namespace TetrisPageRank.Services
{
    public class PieceIService : PieceService
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

            for (int i = 0; i <= 5; i++)
            {
                if (IsDrop0Possible(digits, i))
                {
                    bestRank = CalculateBestRank(Drop0(stack, i), bestRank);
                }
            }

            for (int i = 0; i <= 8; i++)
            {
                bestRank = CalculateBestRank(Drop90(stack, i), bestRank);
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

            for (int i = 0; i <= 5; i++)
            {
                if (IsDrop0Possible(digits, i))
                {
                    possibleDrops.Add(new TetrisDrop(Drop0(stack, i), 0, i, Piece.I));
                }
            }

            for (int i = 0; i <= 8; i++)
            {
                possibleDrops.Add(new TetrisDrop(Drop90(stack, i), 90, i, Piece.I));
            }

            return possibleDrops;
        }

        private static int Drop0(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +1);
            }

            if (column != 5)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 3, -1);
            }

            return stack;
        }

        private static int Drop90(int stack, int column)
        {
            if (column != 0)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column - 1, +4);
            }

            if (column != 8)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column, -4);
            }

            return stack;
        }

        private static bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0 && readableDigits[column + 1] == 0 && readableDigits[column + 2] == 0;
        }
    }
}