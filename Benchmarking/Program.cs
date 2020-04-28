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
            var r = new AlfaBetaSearchBenchmark();
            var r1 = r.EvaluateTreeSerial();

            BenchmarkRunner.Run<AlfaBetaSearchBenchmark>();
        }
    }
}
