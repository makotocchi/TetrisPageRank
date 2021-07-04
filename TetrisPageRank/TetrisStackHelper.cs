using System;
using System.Linq;
using System.Text;

namespace TetrisPageRank
{
    public static class TetrisStackHelper
    {
        public static int CreateStack(int a, int b, int c, int d, int e, int f, int g, int h)
        {
            return a + 4 << 28 |
                   b + 4 << 24 |
                   c + 4 << 20 |
                   d + 4 << 16 |
                   e + 4 << 12 |
                   f + 4 << 8 |
                   g + 4 << 4 |
                   h + 4;
        }

        public static int GetReadableDigit(int stack, int column)
        {
            return (stack >> (28 - column * 4) & 0xF) - 4;
        }

        public static int SetColumnHeight(int stack, int column, int height)
        {
            height += 4;
            var bitsToShift = (8 - column) * 4 - 4;

            if (height > 8)
            {
                height = 8;
            }
            else if (height < 0)
            {
                height = 0;
            }

            var shiftedHeight = height << bitsToShift;
            return stack & ~(0xF << bitsToShift) | shiftedHeight;
        }

        public static int AlterColumnHeight(int stack, int column, int units)
        {
            var bitsToShift = (8 - column) * 4 - 4;

            var columnHeight = stack >> bitsToShift & 0xF;

            var futureHeight = columnHeight + units;

            if (futureHeight > 8)
            {
                futureHeight = 8;
            }
            else if (futureHeight < 0)
            {
                futureHeight = 0;
            }

            columnHeight = futureHeight << bitsToShift;
            return stack & ~(0xF << bitsToShift) | columnHeight;
        }

        public static string StackToString(int a, int b, int c, int d, int e, int f, int g, int h)
        {
            ReadOnlySpan<int> digits = stackalloc[] { a, b, c, d, e, f, g, h };

            var surface = Enumerable.Repeat(string.Empty, 9).ToArray();

            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i] < 0)
                {
                    while (surface[i].Length + digits[i] < 0)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            surface[j] += "*";
                        }
                    }

                    surface[i + 1] = new string('*', surface[i].Length + digits[i]);
                }

                if (digits[i] == 0)
                {
                    surface[i + 1] = surface[i];
                }

                if (digits[i] > 0)
                {
                    surface[i + 1] = new string('*', surface[i].Length + digits[i]);
                }
            }

            var sb = new StringBuilder();
            foreach (var col in surface)
            {
                sb.AppendFormat("|{0}", col);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
