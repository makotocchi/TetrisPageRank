using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TetrisPageRank;
using TetrisPageRank.Models;

namespace KeyBloxManager
{
    public class Program
    {
        private static void Main()
        {
            var keyblox = new KeyBlox();

            int[] columns = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int stack = CreateNewStack();

            Ranker ranker = new Ranker(@"C:\Users\Renam\Desktop\final_test_fix.json");

            while (true)
            {
                Thread.Sleep(1000);

                Piece piece = Piece.AllDictionary[keyblox.GetCurrentPiece().GetName()];

                if (piece.Name == 'I' && columns.All(x => x > 5))
                {
                    keyblox.DoTetris();
                    ClearLines(columns, 4);
                    continue;
                }

                if (columns[7] - columns[8] > 5 && columns.Take(8).All(x => x > 5))
                {
                    if (piece.Name == 'T' || piece.Name == 'L' || piece.Name == 'J')
                    {
                        keyblox.RotatePiece(270);
                        keyblox.DropPiece(8);
                        ClearLines(columns, 1);
                        continue;
                    }
                }

                var possibleDrops = piece.Controller.GetPossibleDrops(stack);

                Piece.AllDictionary.TryGetValue(keyblox.GetHeldPiece().GetName(), out var heldPiece);

                if (heldPiece != null && heldPiece.Name == 'I' && columns.All(x => x > 5))
                {
                    keyblox.HoldPiece();
                    keyblox.DoTetris();
                    ClearLines(columns, 4);
                    continue;
                }

                List<Piece> lookahead = keyblox.GetPreviewedPieces().Select(x => Piece.AllDictionary[x.GetName()]).ToList();
                
                if (heldPiece != null)
                {
                    possibleDrops = possibleDrops.Concat(heldPiece.Controller.GetPossibleDrops(stack));
                }
                else
                {
                    possibleDrops = possibleDrops.Concat(lookahead[0].Controller.GetPossibleDrops(stack));
                }

                TetrisDrop bestDrop = GetBestDrop(ranker, possibleDrops);

                if (bestDrop != null)
                {
                    if (bestDrop.Piece == heldPiece)
                    {
                        keyblox.HoldPiece();
                        piece = heldPiece;
                    }
                    else if (bestDrop.Piece == lookahead[0] && bestDrop.Piece != piece)
                    {
                        keyblox.HoldPiece();
                        piece = lookahead[0];
                    }

                    DropPiece(keyblox, bestDrop);
                    UpdateColumns(piece, bestDrop, columns);
                    stack = UpdateStack(columns);
                }
                else
                {
                    keyblox.HoldPiece();
                }
            }
        }

        private static void DropPiece(KeyBlox keyblox, TetrisDrop bestDrop)
        {
            keyblox.RotatePiece(bestDrop.Orientation);
            keyblox.DropPiece(bestDrop.Column);
        }

        private static int CreateNewStack()
        {
            return TetrisStack.CreateStack(new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });
        }

        private static TetrisDrop GetBestDrop(Ranker ranker, IEnumerable<TetrisDrop> possibleDrops)
        {
            TetrisDrop bestDrop = null;
            float bestRank = 0;

            foreach (var drop in possibleDrops)
            {
                var rank = ranker.CurrentIteration.stackRanks[drop.TetrisStack];

                if (rank > bestRank)
                {
                    bestRank = rank;
                    bestDrop = drop;
                }
            }

            return bestDrop;
        }

        private static TetrisDrop GetBestDropWithLookAhead(Ranker ranker, IEnumerable<TetrisDrop> possibleDrops, IEnumerable<Piece> preview)
        {
            if (preview.Count() == 0)
            {
                return GetBestDrop(ranker, possibleDrops);
            }

            TetrisDrop bestDrop = possibleDrops.First();
            float bestRank = 0;

            foreach (var drop in possibleDrops)
            {
                var futurePossibleDrops = preview.First().Controller.GetPossibleDrops(drop.TetrisStack);
                if (!futurePossibleDrops.Any())
                {
                    continue;
                }

                var currentDrop = GetBestDropWithLookAhead(ranker, futurePossibleDrops, preview.Skip(1));

                if (currentDrop == null)
                {
                    continue;
                }

                var rank = ranker.CurrentIteration.stackRanks[currentDrop.TetrisStack];

                if (rank > bestRank)
                {
                    bestRank = rank;
                    bestDrop = drop;
                }
            }

            return bestDrop;
        }

        public static void UpdateColumns(Piece piece, TetrisDrop drop, int[] columns)
        {
            int[] heightDeltas = new int[] { };

            if (piece == Piece.I)
            {
                if (drop.Orientation == 0)
                {
                    heightDeltas = new int[] { 1, 1, 1, 1 };
                }

                if (drop.Orientation == 90)
                {
                    heightDeltas = new int[] { 4 };
                }
            }

            if (piece == Piece.L)
            {
                if (drop.Orientation == 0)
                {
                    heightDeltas = new int[] { 1, 1, 2 };
                }

                if (drop.Orientation == 90)
                {
                    heightDeltas = new int[] { 1, 3 };
                }

                if (drop.Orientation == 180)
                {
                    heightDeltas = new int[] { 2, 1, 1 };
                }

                if (drop.Orientation == 270)
                {
                    heightDeltas = new int[] { 3, 1 };
                }
            }

            if (piece == Piece.O)
            {
                heightDeltas = new int[] { 2, 2 };
            }

            if (piece == Piece.Z)
            {
                if (drop.Orientation == 0)
                {
                    heightDeltas = new int[] { 1, 2, 1 };
                }

                if (drop.Orientation == 90)
                {
                    heightDeltas = new int[] { 2, 2 };
                }
            }

            if (piece == Piece.T)
            {
                if (drop.Orientation == 0)
                {
                    heightDeltas = new int[] { 1, 2, 1 };
                }

                if (drop.Orientation == 90)
                {
                    heightDeltas = new int[] { 1, 3 };
                }

                if (drop.Orientation == 180)
                {
                    heightDeltas = new int[] { 1, 2, 1 };
                }

                if (drop.Orientation == 270)
                {
                    heightDeltas = new int[] { 3, 1 };
                }
            }

            if (piece == Piece.J)
            {
                if (drop.Orientation == 0)
                {
                    heightDeltas = new int[] { 2, 1, 1 };
                }

                if (drop.Orientation == 90)
                {
                    heightDeltas = new int[] { 1, 3 };
                }

                if (drop.Orientation == 180)
                {
                    heightDeltas = new int[] { 1, 1, 2 };
                }

                if (drop.Orientation == 270)
                {
                    heightDeltas = new int[] { 3, 1 };
                }
            }

            if (piece == Piece.S)
            {
                if (drop.Orientation == 0)
                {
                    heightDeltas = new int[] { 1, 2, 1 };
                }

                if (drop.Orientation == 90)
                {
                    heightDeltas = new int[] { 2, 2 };
                }
            }

            var columnIndex = drop.Column;
            for (int i = 0; i < heightDeltas.Length; i++)
            {
                columns[columnIndex++] += heightDeltas[i];
            }
        }

        public static int UpdateStack(int[] columns)
        {
            var stack = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 1; i < columns.Length; i++)
            {
                stack[i - 1] = columns[i] - columns[i - 1];

                if (stack[i - 1] > 4)
                {
                    stack[i - 1] = 4;
                }

                if (stack[i - 1] < -4)
                {
                    stack[i - 1] = -4;
                }
            }

            return TetrisStack.CreateStack(stack);
        }

        public static void ClearLines(int[] columns, int n)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] -= n;
            }
        }
    }
}