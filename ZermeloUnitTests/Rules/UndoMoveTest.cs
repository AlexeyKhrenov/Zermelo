﻿using System.Collections.Generic;
using System.Linq;
using Checkers;
using Game.Implementations;
using Game.Primitives;
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
            var sourceBoardStr = new[]
            {
                "_b_b",
                "____",
                "___w",
                "w___"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var targetBoardStr = new[]
            {
                "_b_b",
                "____",
                "____",
                "w_w_"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.Player1.Figures.First(f => f.X == 0 && f.Y == 3).AvailableMoves =
                new List<Cell> {new Cell(1, 2)};
            targetBoard.Player1.Figures.First(f => f.X == 2 && f.Y == 3).AvailableMoves =
                new List<Cell> {new Cell(1, 2), new Cell(3, 2)};

            var move = new HistoryItem(sourceBoard.AwaitingPlayer, new Move(new Cell(2, 3), new Cell(3, 2)));

            // ACT
            _rules.Undo(sourceBoard, move, null);

            // ASSERT
            AssertBoardsAreEqual(targetBoard, sourceBoard);
        }

        [Fact]
        public void UndoMovesTest_2()
        {
            // ARRANGE
            var sourceBoardStr = new[]
            {
                "___b",
                "w___",
                "____",
                "w___"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var capturedPiece = new Piece(1, 2, false, false, false);
            capturedPiece.IsCaptured = true;
            sourceBoard.Player2.Figures.Add(capturedPiece);

            var targetBoardStr = new[]
            {
                "___b",
                "____",
                "_b__",
                "w_w_"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.Player1.Figures.First(f => f.X == 0 && f.Y == 3).AvailableMoves =
                new List<Cell> {new Cell(2, 1)};
            targetBoard.Player1.Figures.First(f => f.X == 2 && f.Y == 3).AvailableMoves =
                new List<Cell> {new Cell(0, 1)};

            var move = new HistoryItem(sourceBoard.AwaitingPlayer, new Move(new Cell(2, 3), new Cell(0, 1)));
            move.Captured = new Piece(1, 2, false, false, true);

            // ACT
            _rules.Undo(sourceBoard, move, null);

            // ASSERT
            AssertBoardsAreEqual(targetBoard, sourceBoard);
        }

        [Fact]
        public void UndoMovesTest_3()
        {
            // ARRANGE
            var sourceBoardStr = new[]
            {
                "___W",
                "____",
                "____",
                "B_w_"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var capturedPiece = new Piece(2, 1, false, false, false, true);
            capturedPiece.IsCaptured = true;
            sourceBoard.Player2.Figures.Add(capturedPiece);

            var targetBoardStr = new[]
            {
                "____",
                "__B_",
                "_w__",
                "B_w_"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);
            targetBoard.Player1.Figures.First(f => f.X == 1 && f.Y == 2).AvailableMoves =
                new List<Cell> {new Cell(3, 0)};

            var move = new HistoryItem(sourceBoard.AwaitingPlayer, new Move(new Cell(1, 2), new Cell(3, 0)));
            move.Captured = new Piece(2, 1, false, false, true, true);
            move.IsPieceChangeType = true;

            // ACT
            _rules.Undo(sourceBoard, move, null);

            // ASSERT
            AssertBoardsAreEqual(targetBoard, sourceBoard);
        }

        [Fact]
        public void UndoMovesTest_4()
        {
            // ARRANGE
            var sourceBoardStr = new[]
            {
                "__W__",
                "_____",
                "_____",
                "_____",
                "____B"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 5, false);
            sourceBoard.SwitchPlayers();

            var capturedPiece = new Piece(1, 1, false, false, false, true);
            capturedPiece.IsCaptured = true;
            sourceBoard.Player2.Figures.Add(capturedPiece);

            var targetBoardStr = new[]
            {
                "_____",
                "_B___",
                "w____",
                "_____",
                "____B"
            };
            var targetBoard = new BoardMock(targetBoardStr, 5, false);
            targetBoard.Player1.Figures.First(f => f.X == 0 && f.Y == 2).AvailableMoves =
                new List<Cell> {new Cell(2, 0)};

            var move = new HistoryItem(sourceBoard.AwaitingPlayer, new Move(0, 2, 2, 0));
            move.Captured = new Piece(1, 1, false, false, true, true);
            move.IsPieceChangeType = true;

            var latestMoveBeforeUnto = new HistoryItem(sourceBoard.AwaitingPlayer, new Move(2, 4, 0, 2));
            latestMoveBeforeUnto.Captured = new Piece(1, 3, false, false, true, true);

            // ACT
            _rules.Undo(sourceBoard, move, latestMoveBeforeUnto);

            // ASSERT
            AssertBoardsAreEqual(targetBoard, sourceBoard);
        }
    }
}