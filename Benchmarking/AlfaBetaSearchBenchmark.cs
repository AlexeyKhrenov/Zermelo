using BenchmarkDotNet.Attributes;

using CheckersAI.ByteTree;
using CheckersAI.TreeSearch;
using System.Threading;

namespace Benchmarking
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AlfaBetaSearchBenchmark
    {
        private SerialAlfaBetaSearch<ByteNode, sbyte, sbyte> _serial;
        private ByteNode _serialTree;
        private CancellationTokenSource _cts;

        public AlfaBetaSearchBenchmark()
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

            _cts = new CancellationTokenSource();
        }

        [Benchmark]
        public sbyte EvaluateTreeSerial()
        {
            return _serial.Search(
                _serialTree,
                _serialTree.GetDepth<ByteNode, sbyte>(),
                sbyte.MinValue,
                sbyte.MaxValue,
                0,
                _cts.Token
            );
        }
    }
}
