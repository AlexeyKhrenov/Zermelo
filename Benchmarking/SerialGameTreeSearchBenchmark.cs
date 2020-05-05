using BenchmarkDotNet.Attributes;
using Checkers.Minifications;
using CheckersAI;
using CheckersAI.ByteTree;
using CheckersAI.CheckersGameTree;
using CheckersAI.TreeSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class SerialGameTreeSearchBenchmark
    {
        public SerialGameTreeSearchBenchmark()
        {
            var _search = ServiceLocator.CreateSerialGameTreeSearch();
        }
    }
}
