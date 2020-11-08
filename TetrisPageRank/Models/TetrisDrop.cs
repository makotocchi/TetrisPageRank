namespace TetrisPageRank.Models
{
    public class TetrisDrop
    {
        public TetrisDrop(int stack, int orientation, int column, Piece piece)
        {
            TetrisStack = stack;
            Orientation = orientation;
            Column = column;
            Piece = piece;
        }

        public int TetrisStack { get; }
        public int Orientation { get; }
        public int Column { get; }
        public Piece Piece { get; }
    }
}
