using System;
using System.Collections.Generic;

namespace TetrisPageRank.Services
{
    public class PieceOService : PieceService
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

            for (int i = 0; i <= 7; i++)
            {
                if (IsDrop0Possible(digits, i))
                {
                    bestRank = CalculateBestRank(Drop0(stack, i), bestRank);
                }
            }

            return bestRank;
        }

        public override List<TetrisDrop> GetPossibleDrops(int[] columns)
        {
            var possibleDrops = new List<TetrisDrop>();

            for (int i = 0; i <= 7; i++)
            {
                if (columns[i] == columns[i + 1] && columns[i] <= 17)
                {
                    var newColumns = new int[columns.Length];
                    Array.Copy(columns, newColumns, columns.Length);

                    newColumns[i] += 2;
                    newColumns[i + 1] += 2;

                    possibleDrops.Add(new TetrisDrop(newColumns, 0, i, Piece.O));
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

            if (column != 7)
            {
                stack = TetrisStackHelper.AlterColumnHeight(stack, column + 1, -2);
            }

            return stack;
        }

        private static bool IsDrop0Possible(ReadOnlySpan<int> readableDigits, int column)
        {
            return readableDigits[column] == 0;
        }
    }
}
