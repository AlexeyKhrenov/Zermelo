using System.Threading;
using Checkers;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using FluentAssertions;
using Game.Primitives;
using Xunit;
using ZermeloUnitTests.Mocks;
using ServiceLocator = CheckersAI.ServiceLocator;

namespace ZermeloUnitTests.GameTreeSearch
{
    public class ProgressiveDeepeningSequentialTest
    {
        public ProgressiveDeepeningSequentialTest()
        {
            _search = ServiceLocator.CreateSerialGameTreeSearch();
            _wrapper = ServiceLocator.CreateProgressiveDeepeningWrapper(_search);
            _rules = new CheckersRules();
            _cts = new CancellationTokenSource();
        }

        private readonly ISearch<GameNode, sbyte, BoardMinified> _search;
        private readonly IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> _wrapper;
        private CheckersRules _rules;
        private readonly CancellationTokenSource _cts;

        [Fact]
        public void DynamicDeepeningSequentialTest_1()
        {
            var sourceBoardStr = new[]
            {
                "B___",
                "____",
                "__w_",
                "____"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            var (result, maxPly) = _wrapper.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, 2, _cts.Token);
            var bestMove = result.Peek().Move;
            bestMove.From.Should().BeEquivalentTo(new Cell(2, 2));
            bestMove.To.Should().BeEquivalentTo(new Cell(3, 1));
        }

        [Fact]
        public void DynamicDeepeningSequentialTest_2()
        {
            // ARRANGE
            var sourceBoardStr = new[]
            {
                "____",
                "_b__",
                "____",
                "_w_w"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            var (result, maxPly) = _wrapper.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, 3, _cts.Token);
            var bestMove = result.Peek().Move;
            bestMove.From.Should().BeEquivalentTo(new Cell(1, 3));
            bestMove.To.Should().BeEquivalentTo(new Cell(0, 2));
        }

        [Fact]
        public void DynamicDeepeningSequentialTest_3()
        {
            // ARRANGE
            var sourceBoardStr = new[]
            {
                "b_b_",
                "____",
                "____",
                "_w_w"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var root = new GameNode();

            var practiceBoard = sourceBoard.ToMinified();

            var (result, maxPly) = _wrapper.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, 12, _cts.Token);
            var bestMove = result.Peek().Move;
            bestMove.From.Should().BeEquivalentTo(new Cell(3, 3));
            bestMove.To.Should().BeEquivalentTo(new Cell(2, 2));
        }

        [Fact]
        public void DynamicDeepeningSequentialTest_4()
        {
            // ARRANGE
            var sourceBoardStr = new[]
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

            var (result, maxPly) = _wrapper.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, 3, _cts.Token);
            var bestMove = result.Peek().Move;
            bestMove.From.Should().BeEquivalentTo(new Cell(2, 5));
            bestMove.To.Should().BeEquivalentTo(new Cell(4, 3));
        }

        [Fact]
        public void DynamicDeepeningSequentialTest_5()
        {
            // ARRANGE
            var sourceBoardStr = new[]
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

            var (result, maxPly) = _wrapper.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, 2, _cts.Token);
            var bestMove = result.Peek().Move;
            bestMove.From.Should().BeEquivalentTo(new Cell(3, 5));
            bestMove.To.Should().BeEquivalentTo(new Cell(1, 3));
        }

        [Fact]
        public void DynamicDeepeningSequentialTest_6()
        {
            // ARRANGE
            var sourceBoardStr = new[]
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

            var (result, maxPly) = _wrapper.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, 3, _cts.Token);
            var bestMove = result.Peek().Move;
            bestMove.From.Should().BeEquivalentTo(new Cell(2, 5));
            bestMove.To.Should().BeEquivalentTo(new Cell(3, 4));
        }

        [Fact]
        public void DynamicDeepeningSequentialTest_7()
        {
            // ARRANGE
            var sourceBoardStr = new[]
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

            var (result, maxPly) = _wrapper.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, 3, _cts.Token);
            var bestMove = result.Peek().Move;
            bestMove.From.Should().BeEquivalentTo(new Cell(1, 0));
            bestMove.To.Should().BeEquivalentTo(new Cell(0, 1));
        }
    }
}