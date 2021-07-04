using BenchmarkDotNet.Running;
using System;

namespace TetrisPageRank.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RankerBenchmark>();
        }
    }
}
