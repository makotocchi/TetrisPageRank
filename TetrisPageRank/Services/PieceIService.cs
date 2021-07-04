using System;
using System.Collections.Generic;
using System.Linq;

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

        public override List<TetrisDrop> GetPossibleDrops(int[] columns)
        {
            var possibleDrops = new List<TetrisDrop>();

            if (columns.All(x => x >= 4))
            {
                var newColumns = new int[columns.Length];
                Array.Copy(columns, newColumns, columns.Length);

                for (int i = 0; i < newColumns.Length; i++)
                {
                    newColumns[i] -= Math.Min(columns.Min(), 4);
                }

                possibleDrops.Add(new TetrisDrop(newColumns, 90, 9, Piece.I));
            }
            else
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (columns[i] == columns[i + 1] && columns[i] == columns[i + 2] && columns[i] == columns[i + 3] && columns[i] <= 18)
                    {
                        var newColumns = new int[columns.Length];
                        Array.Copy(columns, newColumns, columns.Length);

                        newColumns[i] += 1;
                        newColumns[i + 1] += 1;
                        newColumns[i + 2] += 1;
                        newColumns[i + 3] += 1;

                        possibleDrops.Add(new TetrisDrop(newColumns, 0, i, Piece.I));
                    }
                }

                for (int i = 0; i <= 8; i++)
                {
                    if (columns[i] <= 15)
                    {
                        var newColumns = new int[columns.Length];
                        Array.Copy(columns, newColumns, columns.Length);

                        newColumns[i] += 4;

                        possibleDrops.Add(new TetrisDrop(newColumns, 90, i, Piece.I));
                    }
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