using BenchmarkDotNet.Attributes;
using Benchmarking.ByteTree;
using CheckersAI.TreeSearch;

namespace Benchmarking.AlfaBetaSingleThreaded
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AlfaBetaSingleThreadedBenchmark
    {
        private AlfaBetaSearch<ByteNode, byte, byte> _search;
        private ByteNode _tree;

        public AlfaBetaSingleThreadedBenchmark()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();
            _search = new AlfaBetaSearch<ByteNode, byte, byte>(evaluator, brancher, comparator, byte.MaxValue, byte.MinValue);

            _tree = TreeGenerator.ReadTree();
        }

        [Benchmark]
        public void EvaluateTree()
        {
            var result = _search.Search(_tree, int.MaxValue);
        }
    }
}
