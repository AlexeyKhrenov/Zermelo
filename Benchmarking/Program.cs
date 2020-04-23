using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Running;
using Benchmarking.AlfaBetaSingleThreaded;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<AlfaBetaSingleThreadedBenchmark>();
        }
    }
}
