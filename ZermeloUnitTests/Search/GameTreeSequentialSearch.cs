using Checkers;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.TreeSearch;
using FluentAssertions;
using Game.Primitives;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.Search
{
    public class GameTreeSequentialSearch
    {
        private AlfaBetaSearch<GameNode, sbyte, BoardMinified> _search;
        private CheckersRules _rules;

        public GameTreeSequentialSearch()
        {
            _rules = new CheckersRules();

            var comparator = new Comparator();
            var brancher = new Brancher(_rules);
            var evaluator = new Evaluator(true);
            var stateTransitions = new StateTransitions(_rules);

            _search = new AlfaBetaSearch<GameNode, sbyte, BoardMinified>(
                evaluator,
                brancher,
                comparator,
                stateTransitions,
                sbyte.MaxValue,
                sbyte.MinValue
            );
        }

        [Fact]
        public void GameTreeSequentialSearch_1()
        {
            // ARRANGE
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
            root.IsMaxPlayer = true;

            var practiceBoard = new BoardMinified();
            practiceBoard.Minify(sourceBoard);

            practiceBoard = _rules.MakeMove(practiceBoard, root.Move);

            _search.Search(root, 1, sbyte.MinValue, sbyte.MaxValue, practiceBoard);
            var bestMove = root.GetBestMove();

            bestMove.Should().BeEquivalentTo(new Move(2, 2, 3, 1));
        }
    }
}
