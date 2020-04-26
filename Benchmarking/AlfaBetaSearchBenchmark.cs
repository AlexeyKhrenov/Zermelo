using BenchmarkDotNet.Attributes;
using Benchmarking.ByteTree;
using CheckersAI.MultithreadedTreeSearch;
using CheckersAI.TreeSearch;

namespace Benchmarking
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AlfaBetaSearchBenchmark
    {
        private AlfaBetaSearch<ByteNode, byte, byte> _oneThread;
        private ByteNode _oneThreadTree;

        private AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte> _multiThreaded;
        private AlfaBetaByteNode _multiThreadedTree;
        private AlfaBetaByteNode[] _multiThreadedTreeToArray;

        public AlfaBetaSearchBenchmark()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();
            
            _oneThread = new AlfaBetaSearch<ByteNode, byte, byte>(evaluator, brancher, comparator, byte.MaxValue, byte.MinValue);
            _multiThreaded = new AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte>(evaluator, brancher, comparator);

            _oneThreadTree = TreeGenerator.ReadTree();
            _multiThreadedTree = TreeGenerator.ReadAlfaBetaByteTree();
            _multiThreadedTreeToArray = _multiThreadedTree.ToList().ToArray();
        }

        [Benchmark]
        public byte EvaluateTree()
        {
            return _oneThread.Search(_oneThreadTree, _oneThreadTree.GetDepth());
        }

        [Benchmark]
        public byte EvaluateTreeMultithreaded()
        {
            _multiThreaded.Search(_multiThreadedTree, _multiThreadedTree.GetDepth());
            var result = _multiThreadedTree.Result;
            _multiThreaded.ClearTree(_multiThreadedTreeToArray, byte.MaxValue, byte.MinValue);
            return result;
        }
    }
}
