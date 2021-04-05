using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Diagnostics;

namespace TetrisPageRank.Benchmark
{
    [SimpleJob(RunStrategy.ColdStart, launchCount:10)]
    public class RankerBenchmark
    {
        Ranker ranker = new Ranker();
        
        [Benchmark]
        public void Iterate()
        {
            ranker.Iterate(10);
        }
    }
}
