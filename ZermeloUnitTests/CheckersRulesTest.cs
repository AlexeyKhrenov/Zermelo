using Checkers;
using FluentAssertions;
using Game.Implementations;
using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests
{
    public class CheckersRulesTest
    {
        IGameRules rules = new CheckersRules(4);

        [Fact]
        public void CheckersRulesTest_1()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "_b_b",
                "____",
                "____",
                "w_w_"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var targetBoardStr = new string[]
            {
                "_b_b",
                "____",
                "___w",
                "w___"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.SwitchPlayers();
            targetBoard.Player2.Figures.First(f => f.X == 1 && f.Y == 0).AvailableMoves =
                new List<Cell>() { new Cell(0, 1), new Cell(2, 1) };
            targetBoard.Player2.Figures.First(f => f.X == 3 && f.Y == 0).AvailableMoves =
                new List<Cell>() { new Cell(2, 1) };

            var move = new HistoryItem(sourceBoard.ActivePlayer, new Move(new Cell(2, 3), new Cell(3, 2)));

            // ACT
            rules.MakeMove(sourceBoard, move);

            // ASSERT
            AssertBoardsAreEqual(targetBoard, sourceBoard);
        }

        [Fact]
        public void CheckersRulesTest_2()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "_b_b",
                "____",
                "_b__",
                "w_w_"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var targetBoardStr = new string[]
            {
                "_b_b",
                "__w_",
                "____",
                "__w_"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.SwitchPlayers();
            targetBoard.Player2.Figures.First(f => f.X == 3 && f.Y == 0).AvailableMoves =
                new List<Cell>() { new Cell(1, 2) };
            targetBoard.Player2.Figures.First(f => f.X == 1 && f.Y == 0).AvailableMoves =
                new List<Cell>() { new Cell(3, 2) };

            var move = new HistoryItem(sourceBoard.ActivePlayer, new Move(new Cell(0, 3), new Cell(2, 1)));

            // ACT
            rules.MakeMove(sourceBoard, move);

            // ASSERT
            AssertBoardsAreEqual(sourceBoard, targetBoard);
        }

        [Fact]
        public void CheckersRulesTest_3()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "___b",
                "__b_",
                "___w",
                "w_w_"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var targetBoardStr = new string[]
            {
                "_W_b",
                "____",
                "____",
                "w_w_"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.SwitchPlayers();
            targetBoard.Player2.Figures.First(f => f.X == 3 && f.Y == 0).AvailableMoves =
                new List<Cell>() { new Cell(2, 1) };

            var move = new HistoryItem(sourceBoard.ActivePlayer, new Move(new Cell(3, 2), new Cell(1, 0)));

            // ACT
            rules.MakeMove(sourceBoard, move);

            // ASSERT
            AssertBoardsAreEqual(sourceBoard, targetBoard);
        }

        [Fact]
        public void CheckersRulesTest_4()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "____",
                "____",
                "_W_b",
                "____"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var targetBoardStr = new string[]
            {
                "____",
                "____",
                "_W__",
                "__B_"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.Player1.Figures.First(f => f.X == 1 && f.Y == 2).AvailableMoves =
                new List<Cell>() { new Cell(0, 3), new Cell(0, 1), new Cell(2, 1),};

            var move = new HistoryItem(sourceBoard.ActivePlayer, new Move(new Cell(3, 2), new Cell(2, 3)));

            // ACT
            rules.MakeMove(sourceBoard, move);

            // ASSERT
            AssertBoardsAreEqual(sourceBoard, targetBoard);
        }

        [Fact]
        public void CheckersRulesTest_5()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "_____",
                "_b___",
                "_____",
                "_b___",
                "__w__"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 5, false);

            var targetBoardStr = new string[]
            {
                "_____",
                "_b___",
                "w____",
                "_____",
                "_____"
            };
            var targetBoard = new BoardMock(targetBoardStr, 5, false);
            targetBoard.Player1.Figures.First(f => f.X == 0 && f.Y == 2).AvailableMoves =
                new List<Cell>() { new Cell(2, 0) };

            var move = new HistoryItem(sourceBoard.ActivePlayer, new Move(new Cell(2, 4), new Cell(0, 2)));

            // ACT
            rules.MakeMove(sourceBoard, move);

            // ASSERT
            AssertBoardsAreEqual(sourceBoard, targetBoard);
        }

        private void AssertBoardsAreEqual(BoardMock a, BoardMock b)
        {
            a.Player1.Figures
                .Should()
                .BeEquivalentTo(b.Player1.Figures);
            
            a.Player2.Figures
                .Should()
                .BeEquivalentTo(b.Player2.Figures);

            a.ActivePlayer.Name.Should().BeEquivalentTo(b.ActivePlayer.Name);
        }
    }
}
