using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TetrisPageRank.Models;

namespace TetrisPageRank
{
    public class Ranker
    {
        public void Iterate(int n)
        {
            if (n <= 0)
            {
                return;
            }

            Console.WriteLine($"Iterating {n} times.");

            var stopwatch = new Stopwatch();

            for (int i = 0; i < n; i++)
            {
                stopwatch.Restart();

                if (i > 0)
                {
                    // Swap ranks list to avoid allocating more memory
                    List<float> aux = Ranks.Current;
                    Ranks.Current = Ranks.Next;
                    Ranks.Next = aux;
                }

                Console.WriteLine($"Iteration {i}");

                //foreach (var index in Ranks.Indexes)
                //{
                //    Ranks.Next[index.Value] = Rank(index.Key);

                //}

                Parallel.ForEach(Ranks.Indexes, index =>
                {
                    // { stack, index } 

                    Ranks.Next[index.Value] = Rank(index.Key);
                });

                stopwatch.Stop();
                Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
            }
        }

        private float Rank(int stack)
        {
            var totalRank = 0f;

            foreach (var piece in Piece.All)
            {
                totalRank += RankPiece(stack, piece);
            }

            return totalRank / Piece.All.Length;
        }

        private float RankPiece(int stack, Piece piece)
        {
            return piece.Controller.GetBestPossibleRank(stack);
        }
    }
}
