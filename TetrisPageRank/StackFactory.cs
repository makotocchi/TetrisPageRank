namespace TetrisPageRank
{
    public class StackFactory
    {
        public int Create(params int[] digits)
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
    }
}
