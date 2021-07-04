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
            //Ranks.InitializeFromFile("ranks500.dat");

            Ranker.Iterate(10);

            Ranks.SaveResults("ranks10.dat");
            
            stopwatch.Stop();

            Log.Information("Time elapsed: {Elapsed}", stopwatch.Elapsed);
            Log.Information("All done.");
        }
    }
}
