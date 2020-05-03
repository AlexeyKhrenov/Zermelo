using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            //var r = new AlfaBetaSearchBenchmark();
            //var r1 = r.EvaluateTreeSerial();

            //BenchmarkRunner.Run<AlfaBetaSearchBenchmark>();
            var s = new struct1();

            //int size = Marshal.SizeOf(new struct1());
        }
    }

    public struct struct1
    {
        public byte a; // 1 byte
        public int b; // 4 bytes
        public short c; // 2 bytes
        public byte d; // 1 byte
    }
}
