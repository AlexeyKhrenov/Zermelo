using System.Runtime.CompilerServices;
using BenchmarkDotNet.Running;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]

namespace Benchmarking
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //MeasureStructSizes.Measure();

            var s = new GameTreeSearchBenchmark();
            s.GlobalSetup();
            //var watch = new Stopwatch();
            //watch.Start();
            //for (var i = 0; i < 10; i++)
            //{
            s.RunDynamicTreeSplittingBenchmark();
            //}
            //watch.Stop();

            BenchmarkRunner.Run<GameTreeSearchBenchmark>();
        }
    }
}