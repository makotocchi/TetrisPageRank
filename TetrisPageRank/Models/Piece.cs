using System.Collections.Generic;
using System.Linq;
using TetrisPageRank.Controllers;

namespace TetrisPageRank.Models
{
    public class Piece
    {
        public char Name { get; }
        public IEnumerable<int> PossibleOrientations { get; }
        public IPieceController Controller { get; }

        private Piece(char name, IEnumerable<int> possibleOrientations, IPieceController controller)
        {
            Name = name;
            PossibleOrientations = possibleOrientations;
            Controller = controller;
        }

        /*       #
         *       #
         *       #
         * ####  #
         */
        public static readonly Piece I = new Piece(
            'I',
            new[] { Orientation.DEGREES_0, Orientation.DEGREES_90 },
            new PieceIController());

        /*      ##       #
         *   #   #  ###  #
         * ###   #  #    ##
         */
        public static readonly Piece L = new Piece(
            'L',
            new[] { Orientation.DEGREES_0, Orientation.DEGREES_90, Orientation.DEGREES_180, Orientation.DEGREES_270 },
            new PieceLController());

        /* ##
         * ##
         */
        public static readonly Piece O = new Piece(
            'O',
            new[] { Orientation.DEGREES_0 },
            new PieceOController());

        /*       #
         * ##   ##
         *  ##  #
         */
        public static readonly Piece Z = new Piece(
            'Z',
            new[] { Orientation.DEGREES_0, Orientation.DEGREES_90 },
            new PieceZController());

        /*       #       #
         *  #   ##  ###  ##
         * ###   #   #   #
         */
        public static readonly Piece T = new Piece(
            'T',
            new[] { Orientation.DEGREES_0, Orientation.DEGREES_90, Orientation.DEGREES_180, Orientation.DEGREES_270 },
            new PieceTController());

        /*       #       ##
         * #     #  ###  #
         * ###  ##    #  #
         */
        public static readonly Piece J = new Piece(
            'J',
            new[] { Orientation.DEGREES_0, Orientation.DEGREES_90, Orientation.DEGREES_180, Orientation.DEGREES_270 },
            new PieceJController());

        /*      #
         *  ##  ##
         * ##    #
         */
        public static readonly Piece S = new Piece(
            'S',
            new[] { Orientation.DEGREES_0, Orientation.DEGREES_90 },
            new PieceSController());

        public static readonly Piece[] All = new Piece[] { I, L, O, Z, T, J, S };
        public static readonly Dictionary<char, Piece> AllDictionary = All.ToDictionary(x => x.Name);
    }
}
