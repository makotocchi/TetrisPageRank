using System.Linq;
using System.Text;

namespace TetrisPageRank
{
    public static class TetrisStack
    {
        public static int CreateStack(params int[] digits)
        {
            return digits[0] + 4 << 28 |
                   digits[1] + 4 << 24 |
                   digits[2] + 4 << 20 |
                   digits[3] + 4 << 16 |
                   digits[4] + 4 << 12 |
                   digits[5] + 4 << 8 |
                   digits[6] + 4 << 4 |
                   digits[7] + 4;
        }

        public static int[] GetReadableDigits(int stack)
        {
            return new[]
            {
                (stack >> 28 & 0xF) - 4,
                (stack >> 24 & 0xF) - 4,
                (stack >> 20 & 0xF) - 4,
                (stack >> 16 & 0xF) - 4,
                (stack >> 12 & 0xF) - 4,
                (stack >> 8 & 0xF) - 4,
                (stack >> 4 & 0xF) - 4,
                (stack & 0xF) - 4
            };
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

        public static int ModifyColumnHeight(int stack, int column, int delta)
        {
            var bitsToShift = (8 - column) * 4 - 4;

            var columnHeight = stack >> bitsToShift & 0xF;

            var futureHeight = columnHeight + delta;

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

        public static string StackToString(int[] digits)
        {
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
