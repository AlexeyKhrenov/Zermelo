using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Running;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            //var r = new AlfaBetaSearchBenchmark();
            ////var r1 = r.EvaluateTree();
            ////var r2 = r.EvaluateTreeAsync();
            //var r3 = r.EvaluateTreeMultithreaded();

            BenchmarkRunner.Run<AlfaBetaSearchBenchmark>();
        }
    }
}
