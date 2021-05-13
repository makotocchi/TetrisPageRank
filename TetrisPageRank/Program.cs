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
            Console.WriteLine("Initializing...");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            {
                Ranks.Initialize();

                var ranker = new Ranker();

                ranker.Iterate(50);

                //Ranks.InitializeFromFile("ranks.dat");
                //Ranks.SaveResults("ranks.dat");
            }
            stopwatch.Stop();

            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
            Console.WriteLine("All done.");
        }
    }
}
