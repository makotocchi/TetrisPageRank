using System.Collections.Generic;
using System.Linq;
using TetrisPageRank.Services;

namespace TetrisPageRank
{
    public class Piece
    {
        public char Name { get; }
        public IEnumerable<int> PossibleOrientations { get; }
        public PieceService Service { get; }

        private Piece(char name, IEnumerable<int> possibleOrientations, PieceService service)
        {
            Name = name;
            PossibleOrientations = possibleOrientations;
            Service = service;
        }

        /*       #
         *       #
         *       #
         * ####  #
         */
        public static readonly Piece I = new(
            'I',
            new[] { 0, 90 },
            new PieceIService());

        /*      ##       #
         *   #   #  ###  #
         * ###   #  #    ##
         */
        public static readonly Piece L = new(
            'L',
            new[] { 0, 90, 180, 270 },
            new PieceLService());

        /* ##
         * ##
         */
        public static readonly Piece O = new(
            'O',
            new[] { 0 },
            new PieceOService());

        /*       #
         * ##   ##
         *  ##  #
         */
        public static readonly Piece Z = new(
            'Z',
            new[] { 0, 90 },
            new PieceZService());

        /*       #       #
         *  #   ##  ###  ##
         * ###   #   #   #
         */
        public static readonly Piece T = new(
            'T',
            new[] { 0, 90, 180, 270 },
            new PieceTService());

        /*       #       ##
         * #     #  ###  #
         * ###  ##    #  #
         */
        public static readonly Piece J = new(
            'J',
            new[] { 0, 90, 180, 270 },
            new PieceJService());

        /*      #
         *  ##  ##
         * ##    #
         */
        public static readonly Piece S = new(
            'S',
            new[] { 0, 90 },
            new PieceSService());

        public static readonly Piece[] All = new Piece[] { I, L, O, Z, T, J, S };
        public static readonly Dictionary<char, Piece> AllDictionary = All.ToDictionary(x => x.Name);

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
