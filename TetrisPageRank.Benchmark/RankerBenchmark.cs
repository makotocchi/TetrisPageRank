using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Diagnostics;

namespace TetrisPageRank.Benchmark
{
    public class RankerBenchmark
    {
        Ranker ranker = new Ranker();
        
        [Benchmark]
        public void Iterate()
        {
            Ranks.Initialize();
            ranker.Iterate(10);
        }
    }
}
