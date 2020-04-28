﻿using BenchmarkDotNet.Attributes;

using CheckersAI.ByteTree;
using CheckersAI.TreeSearch;

namespace Benchmarking
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AlfaBetaSearchBenchmark
    {
        private AlfaBetaSearch<ByteNode, sbyte> _serial;
        private ByteNode _serialTree;

        public AlfaBetaSearchBenchmark()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();
            
            _serial = new AlfaBetaSearch<ByteNode, sbyte>(evaluator, brancher, comparator, sbyte.MaxValue, sbyte.MinValue);
        }

        [Benchmark]
        public sbyte EvaluateTreeSerial()
        {
            return _serial.Search(
                _serialTree,
                _serialTree.GetDepth<ByteNode, sbyte>(),
                sbyte.MinValue,
                sbyte.MaxValue
            );
        }
    }
}
