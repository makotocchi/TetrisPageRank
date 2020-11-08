using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace TetrisPageRank
{
    public class Program
    {
        public static void Main()
        {
            //var filename = @"C:\Users\Renam\Documents\GitHub\tetrisrank\test_n1_output.txt";

            //var engine = new FileHelperAsyncEngine<RankingResult>();
            //var dict = new Dictionary<string, float>();
            //using (engine.BeginReadFile(filename))
            //{
            //    foreach (RankingResult rankingResult in engine)
            //    {
            //        dict.Add(rankingResult.Surface, rankingResult.Rank);
            //    }
            //}

            Console.WriteLine("Initializing...");

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var filename = @"C:\Users\Renam\Desktop\final_test_fix.json";
            var pageRank = new Ranker(filename);

            Console.WriteLine($"Current iteration: {pageRank.CurrentIteration.iterationCount}");

            stopwatch.Stop();

            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");

            pageRank.Iterate(50);
            pageRank.CurrentIteration.Save(filename);

            Console.WriteLine("All done.");

            //var surface = new int[] { 0, 2, -2, 1, 0, -1, 0, 0 };

            //PrintSurface(surface);

            //var intSurface = TetrisStack.Create(surface);

            //var controller = new PieceJController();
            //var possibleSurfaces = controller.GetPossibleDrops(intSurface);

            //foreach (var item in possibleSurfaces)
            //{
            //    Console.WriteLine($"Column {item.Column}");
            //    Console.WriteLine($"Orientation {item.Orientation} degrees");
            //    PrintSurface(TetrisStack.GetReadableDigits(item.TetrisStack));
            //}
        }
    }
}
