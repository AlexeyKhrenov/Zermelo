using Checkers;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.TreeSearch;
using FluentAssertions;
using Game.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.Search
{
    public class GameTreeSequentialSearchTest
    {
        private SerialAlfaBetaSearch<GameNode, sbyte, BoardMinified> _search;
        private CheckersRules _rules;
        private CancellationTokenSource _cts;

        public GameTreeSequentialSearchTest()
        {
            _search = CheckersAI.ServiceLocator.CreateSerialGameTreeSearch();
            _rules = new CheckersRules();
            _cts = new CancellationTokenSource();
        }

        [Fact]
        public void GameTreeSequentialSearch_1()
        {
            var sourceBoardStr = new string[]
            {
                "____",
                "_B__",
                "__w_",
                "____"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var root = new GameNode();
            root.Move = new HistoryItemMinified(new Cell(1, 1), new Cell(0, 0), false);

            var practiceBoard = new BoardMinified();
            practiceBoard.Minify(sourceBoard);

            practiceBoard = _rules.MakeMove(practiceBoard, root.Move);

            _search.Search(root, 2, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(2, 2, 3, 1));
        }

        [Fact]
        public void GameTreeSequentialSearch_2()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "b___",
                "____",
                "____",
                "_w_w"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var root = new GameNode();
            root.Move = new HistoryItemMinified(new Cell(0, 0), new Cell(1, 1), false);

            var practiceBoard = new BoardMinified();
            practiceBoard.Minify(sourceBoard);

            practiceBoard = _rules.MakeMove(practiceBoard, root.Move);

            _search.Search(root, 3, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(1, 3, 0, 2));
        }

        [Fact]
        public void GameTreeSequentialSearch_3()
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
            root.Move = null;

            var practiceBoard = new BoardMinified();
            practiceBoard.Minify(sourceBoard);
            practiceBoard.Player1Pieces.First(f => f.X == 1 && f.Y == 3).AvailableMoves =
                new List<Cell>() { new Cell(0, 2), new Cell(2, 2) };
            practiceBoard.Player1Pieces.First(f => f.X == 3 && f.Y == 3).AvailableMoves =
                new List<Cell>() { new Cell(2, 2) };

            _search.Search(root, 12, sbyte.MinValue, sbyte.MaxValue, practiceBoard, _cts.Token);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(1, 3, 2, 2));
        }
    }
}
