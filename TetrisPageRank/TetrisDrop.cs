using System.Collections.Generic;

namespace TetrisPageRank
{
    public class TetrisDrop
    {
        public TetrisDrop(int[] columns, int orientation, int column, Piece piece)
        {
            Columns = columns;
            Orientation = orientation;
            Column = column;
            Piece = piece;
        }

        public int[] Columns { get; }
        public int Orientation { get; }
        public int Column { get; }
        public Piece Piece { get; }

        public override string ToString()
        {
            return $"{Piece} {Orientation} {Column}";
        }
    }
}
