using System.Runtime.InteropServices;

namespace KeyBloxManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyBloxPiece
    {
        public readonly int Id;
        public readonly int Orientation;
        
        public static int Size => Marshal.SizeOf(typeof(KeyBloxPiece));
        
        public KeyBloxPiece(int id, int orientation)
        {
            Id = id;
            Orientation = orientation;
        }

        public char GetName()
        {
            return Id switch
            {
                PIECE_I => 'I',
                PIECE_O => 'O',
                PIECE_T => 'T',
                PIECE_L => 'L',
                PIECE_J => 'J',
                PIECE_S => 'S',
                PIECE_Z => 'Z',
                _ => ' ',
            };
        }

        private const int PIECE_I = 0;
        private const int PIECE_O = 1;
        private const int PIECE_T = 2;
        private const int PIECE_L = 3;
        private const int PIECE_J = 4;
        private const int PIECE_S = 5;
        private const int PIECE_Z = 6;
    }
}