using BenchmarkDotNet.Attributes;

using CheckersAI.ByteTree;
using CheckersAI.MultithreadedTreeSearch;
using CheckersAI.TreeSearch;

namespace Benchmarking
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AlfaBetaSearchBenchmark
    {
        private AlfaBetaSearch<ByteNode, sbyte, sbyte> _oneThread;
        private ByteNode _oneThreadTree;

        private AlfaBetaSearchMultithreaded<AlfaBetaByteNode, sbyte, sbyte> _multiThreaded;
        private AlfaBetaByteNode _multiThreadedTree;
        private AlfaBetaByteNode[] _multiThreadedTreeToArray;

        public AlfaBetaSearchBenchmark()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();
            
            _oneThread = new AlfaBetaSearch<ByteNode, sbyte, sbyte>(evaluator, brancher, comparator, sbyte.MaxValue, sbyte.MinValue);
            _multiThreaded = new AlfaBetaSearchMultithreaded<AlfaBetaByteNode, sbyte, sbyte>(evaluator, brancher);

            _oneThreadTree = TreeGenerator.ReadTree();
            _multiThreadedTree = TreeGenerator.ReadAlfaBetaByteTree();
            _multiThreadedTreeToArray = _multiThreadedTree.ToList().ToArray();
        }

        [Benchmark]
        public sbyte EvaluateTree()
        {
            return _oneThread.Search(_oneThreadTree, _oneThreadTree.GetDepth());
        }

        [Benchmark]
        public sbyte EvaluateTreeMultithreaded()
        {
            _multiThreaded.Search(_multiThreadedTree, _multiThreadedTree.GetDepth());
            var result = _multiThreadedTree.Result;
            _multiThreaded.ClearTree(_multiThreadedTreeToArray, sbyte.MaxValue, sbyte.MinValue);
            return result;
        }
    }
}
