using BenchmarkDotNet.Running;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<ByteTreeSearchBenchmarks>();
            var t = new SerialGameTreeSearchBenchmark();
            t.GlobalSetup();
            t.RunSerialGameTreeSearchBenchmark();
            BenchmarkRunner.Run<SerialGameTreeSearchBenchmark>();
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
