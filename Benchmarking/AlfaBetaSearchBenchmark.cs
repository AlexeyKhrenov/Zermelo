using BenchmarkDotNet.Attributes;
using Benchmarking.ByteTree;
using CheckersAI.AsyncTreeSearch;
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

        private AlfaBetaSearchTaskBased<AlfaBetaByteNode, byte, byte> _tasks;
        private AlfaBetaByteNode _tasksTree;
        private AlfaBetaByteNode[] _tasksTreeToArray;

        private AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte> _multiThreaded;
        private AlfaBetaByteNode _multiThreadedTree;
        private AlfaBetaByteNode[] _multiThreadedTreeToArray;

        public AlfaBetaSearchBenchmark()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();
            
            _oneThread = new AlfaBetaSearch<ByteNode, byte, byte>(evaluator, brancher, comparator, byte.MaxValue, byte.MinValue);
            _tasks = new AlfaBetaSearchTaskBased<AlfaBetaByteNode, byte, byte>(evaluator, brancher, comparator);
            _multiThreaded = new AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte>(evaluator, brancher, comparator);

            _oneThreadTree = TreeGenerator.ReadTree();
            _tasksTree = TreeGenerator.ReadAlfaBetaByteTree();
            _tasksTreeToArray = _tasksTree.ToList().ToArray();
            _multiThreadedTree = TreeGenerator.ReadAlfaBetaByteTree();
            _multiThreadedTreeToArray = _multiThreadedTree.ToList().ToArray();
        }

        [Benchmark]
        public byte EvaluateTree()
        {
            return _oneThread.Search(_oneThreadTree, _oneThreadTree.GetDepth());
        }

        [Benchmark]
        public byte EvaluateTreeAsync()
        {
            _tasks.Search(_tasksTree, _tasksTree.GetDepth());
            var result = _tasksTree.IsMaxPlayer ? _tasksTree.Alfa : _tasksTree.Beta;
            _tasks.ClearTree(_tasksTreeToArray, byte.MaxValue, byte.MinValue);
            return result;
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
