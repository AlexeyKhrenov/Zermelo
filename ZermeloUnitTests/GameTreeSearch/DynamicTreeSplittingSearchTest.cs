using Checkers;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.TreeSearch;
using FluentAssertions;
using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.GameTreeSearch
{
    public class DynamicTreeSplittingSearchTest
    {
        private DynamicTreeSplitting<GameNode, sbyte, BoardMinified> _search;
        private CheckersRules _rules;
        private CancellationTokenSource _cts;

        public DynamicTreeSplittingSearchTest()
        {
            _search = CheckersAI.ServiceLocator.CreateDynamicTreeSplittingGameTreeSearch();
            _rules = new CheckersRules();
            _cts = new CancellationTokenSource();
        }

        [Fact]
        public void DynamicTreeSplittingSearchTest_1()
        {
            var sourceBoardStr = new string[]
            {
                "B___",
                "____",
                "__w_",
                "____"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            _search.Search(root, 2, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(2, 2, 3, 1));
        }

        [Fact]
        public void DynamicTreeSplittingSearchTest_2()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "____",
                "_b__",
                "____",
                "_w_w"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            _search.Search(root, 3, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(1, 3, 0, 2));
        }

        [Fact]
        public void DynamicTreeSplittingSearchTest_3()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "b_b_",
                "____",
                "____",
                "_w_w"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            _search.Search(root, 12, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(1, 3, 2, 2));
        }

        [Fact]
        public void DynamicTreeSplittingSearchTest_4()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "___b__",
                "______",
                "_b____",
                "______",
                "_b_b__",
                "__w___"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 6, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            _search.Search(root, 3, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(2, 5, 4, 3));
        }

        [Fact]
        public void DynamicTreeSplittingSearchTest_5()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "__b___",
                "______",
                "__b___",
                "______",
                "__b_b_",
                "___w__"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 6, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            _search.Search(root, 2, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(3, 5, 1, 3));
        }

        [Fact]
        public void DynamicTreeSplittingSearchTest_6()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "______",
                "______",
                "______",
                "w_b___",
                "______",
                "w_w_w_"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 6, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            _search.Search(root, 3, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            root.Result.Should().Be(sbyte.MaxValue);
            bestMove.Should().BeEquivalentTo(new Move(2, 5, 3, 4));
        }

        [Fact]
        public void DynamicTreeSplittingSearchTest_7()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "_b_b_b",
                "______",
                "___w_b",
                "______",
                "______",
                "______"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 6, false);
            sourceBoard.SwitchPlayers();

            var root = new GameNode();
            root.IsMaxPlayer = false;

            var practiceBoard = sourceBoard.ToMinified();

            _search.Search(root, 3, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            root.Result.Should().Be(sbyte.MinValue);
            bestMove.Should().BeEquivalentTo(new Move(1, 0, 0, 1));
        }
    }
}
