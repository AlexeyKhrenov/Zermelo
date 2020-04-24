using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Running;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = new AlfaBetaSearchBenchmark().EvaluateTree();
            var r2 = new AlfaBetaSearchBenchmark().EvaluateTreeAsync();
            Console.WriteLine(r1);
            Console.WriteLine(r2);
            Console.ReadKey();
            //BenchmarkRunner.Run<AlfaBetaSingleThreadedBenchmark>();
        }
    }
}
