using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CheckersAI.ByteTree;
using CheckersAI.TreeSearch;
using ZermeloUnitTests.Search;

namespace Benchmarking
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class ByteTreeSearchBenchmarks
    {
        private readonly CancellationTokenSource _cts;

        private readonly DynamicTreeSplitting<AlfaBetaByteNode, sbyte, sbyte> _dynamic;
        private readonly AlfaBetaByteNode _dynamicTree;
        private readonly SerialAlfaBetaSearch<ByteNode, sbyte, sbyte> _serial;
        private readonly ByteNode _serialTree;

        public ByteTreeSearchBenchmarks()
        {
            var comparator = new Comparator();
            var brancher = new Brancher();
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions();

            _serial = new SerialAlfaBetaSearch<ByteNode, sbyte, sbyte>(
                evaluator,
                brancher,
                comparator,
                stateTransitions,
                sbyte.MaxValue,
                sbyte.MinValue);

            _serialTree = TreeGenerator.ReadTree();

            _dynamic = new DynamicTreeSplitting<AlfaBetaByteNode, sbyte, sbyte>(
                evaluator,
                brancher,
                comparator,
                stateTransitions
            );

            _dynamicTree = TreeGenerator.ReadAlfaBetaTree();

            _cts = new CancellationTokenSource();
        }

        [Benchmark]
        public sbyte SerialTreeSearch()
        {
            var result = _serial.Search(
                _serialTree,
                _serialTree.GetDepth<ByteNode, sbyte>(),
                sbyte.MinValue,
                sbyte.MaxValue,
                0,
                _cts.Token
            );

            ProgressiveDeepeningWrapper<ByteNode, sbyte, sbyte>.ClearTree(_serialTree);
            return result;
        }

        [Benchmark]
        public sbyte DynamicTreeSearch()
        {
            var result = _dynamic.Search(
                _dynamicTree,
                _dynamicTree.GetDepth<AlfaBetaByteNode, sbyte>(),
                sbyte.MinValue,
                sbyte.MaxValue,
                0,
                _cts.Token
            );

            ProgressiveDeepeningWrapper<AlfaBetaByteNode, sbyte, sbyte>.ClearTree(_dynamicTree);
            return result;
        }
    }
}