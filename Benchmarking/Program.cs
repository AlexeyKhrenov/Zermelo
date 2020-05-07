using BenchmarkDotNet.Running;
using Checkers.Minifications;
using Game.Primitives;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            //var s = new SerialGameTreeSearchBenchmark();
            //s.GlobalSetup();
            //s.RunSerialGameTreeSearchBenchmark();

            BenchmarkRunner.Run<SerialGameTreeSearchBenchmark>();

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
