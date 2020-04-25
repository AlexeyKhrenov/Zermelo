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

        private AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte> _tasks;
        private AlfaBetaByteNode _tasksTree;
        private AlfaBetaByteNode[] _tasksTreeToArray;

        public AlfaBetaSearchBenchmark()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();
            _oneThread = new AlfaBetaSearch<ByteNode, byte, byte>(evaluator, brancher, comparator, byte.MaxValue, byte.MinValue);
            _tasks = new AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte>(evaluator, brancher, comparator);

            _oneThreadTree = TreeGenerator.ReadTree();
            _tasksTree = TreeGenerator.ReadAlfaBetaByteTree();
            _tasksTreeToArray = _tasksTree.ToList().ToArray();
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
    }
}
