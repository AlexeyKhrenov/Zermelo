using BenchmarkDotNet.Attributes;
using Checkers.Minifications;
using CheckersAI;
using CheckersAI.ByteTree;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using CheckersAI.TreeSearch;
using System;
using System.Threading;
using ZermeloUnitTests.Mocks;

namespace Benchmarking
{
    [PlainExporter]
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class GameTreeSearchBenchmark
    {
        GameNode node1;
        GameNode node2;

        BoardMinified practiceBoard1;
        BoardMinified practiceBoard2;

        ISearch<GameNode, sbyte, BoardMinified> search1;
        ISearch<GameNode, sbyte, BoardMinified> search2;

        IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> wrapper1;
        IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> wrapper2;

        CancellationTokenSource cts;

        [GlobalSetup]
        public void GlobalSetup()
        {
            search1 = ServiceLocator.CreateSerialGameTreeSearch();
            search2 = ServiceLocator.CreateDynamicTreeSplittingGameTreeSearch();

            wrapper1 = ServiceLocator.CreateProgressiveDeepeningWrapper(search1);
            wrapper2 = ServiceLocator.CreateProgressiveDeepeningWrapper(search2);

            var sourceBoardStr = new string[]
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

            practiceBoard1 = sourceBoard1.ToMinified();
            practiceBoard2 = sourceBoard2.ToMinified();

            cts = new CancellationTokenSource();

            node1 = new GameNode();
            node2 = new GameNode();
        }

        [Benchmark]
        public void RunSerialGameTreeSearchBenchmark()
        {
            var result = wrapper1.Run(practiceBoard1, node1, sbyte.MinValue, sbyte.MaxValue, 10, cts.Token);
            node1.Children = null;
            GC.Collect();
        }

        [Benchmark]
        public void RunDynamicTreeSplittingBenchmark()
        {
            var result = wrapper2.Run(practiceBoard1, node2, sbyte.MinValue, sbyte.MaxValue, 10, cts.Token);
            node2.Children = null;
            GC.Collect();
        }
    }
}
