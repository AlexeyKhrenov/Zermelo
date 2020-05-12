using BenchmarkDotNet.Running;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using Game.Primitives;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            //MeasureStructSizes.Measure();

            //var watch = new Stopwatch();
            //watch.Start();
            //for (var i = 0; i < 10; i++)
            //{
            var s = new GameTreeSearchBenchmark();
            s.GlobalSetup();
            s.RunDynamicTreeSplittingBenchmark();
            //}
            //watch.Stop();

            //var piece = new PieceMinified();
            //piece.X = 6;
            //piece.Y = 4;
            //piece.IsWhite = true;

            //BenchmarkRunner.Run<GameTreeSearchBenchmark>();

            //BenchmarkRunner.Run<ByteTreeSearchBenchmarks>();
            //var s = new ByteTreeSearchBenchmarks();
            //var r1 = s.SerialTreeSearch();
            //var r2 = s.DynamicTreeSearch();

            //var r3 = s.SerialTreeSearch();
            //var r4 = s.DynamicTreeSearch();

            //var r5 = s.DynamicTreeSearch();
            //var r6 = s.DynamicTreeSearch();

            //Console.WriteLine(r1);
            //Console.WriteLine(r2);
            //Console.WriteLine(r3);
            //Console.WriteLine(r4);
            //Console.WriteLine(r5);
            //Console.WriteLine(r6);
            //Console.ReadLine();
        }
    }
}
