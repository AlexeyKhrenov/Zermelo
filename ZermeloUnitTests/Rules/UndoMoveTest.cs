using Game.Implementations;
using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.Rules
{
    public class UndoMoveTest : RulesTestBase
    {
        [Fact]
        public void UndoMovesTest_1()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "_b_b",
                "____",
                "___w",
                "w___"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var targetBoardStr = new string[]
            {
                "_b_b",
                "____",
                "____",
                "w_w_"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.Player1.Figures.First(f => f.X == 0 && f.Y == 3).AvailableMoves =
                new List<Cell>() { new Cell(1, 2) };
            targetBoard.Player1.Figures.First(f => f.X == 2 && f.Y == 3).AvailableMoves =
                new List<Cell>() { new Cell(1, 2), new Cell(3, 2)};

            var move = new HistoryItem(sourceBoard.AwaitingPlayer, new Move(new Cell(2, 3), new Cell(3, 2)));

            // ACT
            _rules.Undo(sourceBoard, move, null);

            // ASSERT
            AssertBoardsAreEqual(targetBoard, sourceBoard);
        }
    }
}
