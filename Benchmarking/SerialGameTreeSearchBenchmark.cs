using BenchmarkDotNet.Attributes;
using Checkers.Minifications;
using CheckersAI;
using CheckersAI.ByteTree;
using CheckersAI.CheckersGameTree;
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
    public class SerialGameTreeSearchBenchmark
    {
        GameNode node;
        BoardMinified practiceBoard;
        SerialAlfaBetaSearch<GameNode, sbyte, BoardMinified> search;
        CancellationTokenSource cts;

        [GlobalSetup]
        public void GlobalSetup()
        {
            search = ServiceLocator.CreateSerialGameTreeSearch();

            var sourceBoardStr = new string[]
            {
                "_b_b_b",
                "b_b_b_",
                "______",
                "______",
                "_w_w_w",
                "w_w_w_"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 6, false);

            practiceBoard = new BoardMinified();
            practiceBoard.Minify(sourceBoard);

            cts = new CancellationTokenSource();

            node = new GameNode();
        }

        [Benchmark]
        public void RunSerialGameTreeSearchBenchmark()
        {
            search.DoProgressiveDeepening(node, practiceBoard, sbyte.MinValue, sbyte.MaxValue, 10, cts.Token);
            node.Children = null;
            GC.Collect();
        }
    }
}
