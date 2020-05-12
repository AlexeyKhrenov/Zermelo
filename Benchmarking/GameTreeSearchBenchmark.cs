using System;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Checkers.Minifications;
using CheckersAI;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using ZermeloUnitTests.Mocks;

namespace Benchmarking
{
    [PlainExporter]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class GameTreeSearchBenchmark
    {
        private CancellationTokenSource _cts;
        private GameNode _node1;
        private GameNode _node2;

        private BoardMinified _practiceBoard1;
        private BoardMinified _practiceBoard2;

        private ISearch<GameNode, sbyte, BoardMinified> _search1;
        private ISearch<GameNode, sbyte, BoardMinified> _search2;

        private IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> _wrapper1;
        private IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> _wrapper2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _search1 = ServiceLocator.CreateSerialGameTreeSearch();
            _search2 = ServiceLocator.CreateDynamicTreeSplittingGameTreeSearch();

            _wrapper1 = ServiceLocator.CreateProgressiveDeepeningWrapper(_search1);
            _wrapper2 = ServiceLocator.CreateProgressiveDeepeningWrapper(_search2);

            var sourceBoardStr = new[]
            {
                "_b_b_b",
                "b_b_b_",
                "______",
                "______",
                "_w_w_w",
                "w_w_w_"
            };
            var sourceBoard1 = new BoardMock(sourceBoardStr, 6, false);
            var sourceBoard2 = new BoardMock(sourceBoardStr, 6, false);

            _practiceBoard1 = sourceBoard1.ToMinified();
            _practiceBoard2 = sourceBoard2.ToMinified();

            _cts = new CancellationTokenSource();

            _node1 = new GameNode();
            _node2 = new GameNode();
        }

        public void RunSerialGameTreeSearchBenchmark()
        {
            var result = _wrapper1.Run(_practiceBoard1, _node1, sbyte.MinValue, sbyte.MaxValue, 10, _cts.Token);
            _node1.Children = null;
            GC.Collect();
        }

        [Benchmark]
        public void RunDynamicTreeSplittingBenchmark()
        {
            var result = _wrapper2.Run(_practiceBoard2, _node2, sbyte.MinValue, sbyte.MaxValue, 10, _cts.Token);
            _node2.Children = null;
            GC.Collect();
        }
    }
}