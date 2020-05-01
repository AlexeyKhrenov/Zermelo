using BenchmarkDotNet.Attributes;

using CheckersAI.ByteTree;
using CheckersAI.TreeSearch;

namespace Benchmarking
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AlfaBetaSearchBenchmark
    {
        private AlfaBetaSearch<ByteNode, sbyte, sbyte> _serial;
        private ByteNode _serialTree;

        public AlfaBetaSearchBenchmark()
        {
            var comparator = new Comparator();
            var brancher = new Brancher();
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions();
            
            _serial = new AlfaBetaSearch<ByteNode, sbyte, sbyte>(
                evaluator,
                brancher,
                comparator,
                stateTransitions,
                sbyte.MaxValue,
                sbyte.MinValue);
            _serialTree = TreeGenerator.ReadTree();
        }

        [Benchmark]
        public sbyte EvaluateTreeSerial()
        {
            return _serial.Search(
                _serialTree,
                _serialTree.GetDepth<ByteNode, sbyte>(),
                sbyte.MinValue,
                sbyte.MaxValue,
                0
            );
        }
    }
}
