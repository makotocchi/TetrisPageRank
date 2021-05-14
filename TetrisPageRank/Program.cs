using Serilog;
using System;
using System.Diagnostics;

namespace TetrisPageRank
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            
            Log.Information("Initializing...");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            Ranks.Initialize();
            //Ranks.InitializeFromFile("ranks100.dat");

            Ranker.Iterate(150);

            //Ranks.SaveResults("ranks250.dat");
            
            stopwatch.Stop();

            Log.Information("Time elapsed: {Elapsed}", stopwatch.Elapsed);
            Console.WriteLine();
            Console.WriteLine("All done.");
        }
    }
}
