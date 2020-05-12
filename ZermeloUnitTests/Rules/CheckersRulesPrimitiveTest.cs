using Checkers;
using Checkers.Minifications;
using FluentAssertions;
using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZermeloUnitTests.PrimitivesMinifications;

namespace ZermeloUnitTests.Rules
{
    public unsafe class CheckersRulesPrimitiveTest
    {
        private CheckersRules _rules;
        private Cell[] _emptyAvailableMoves;

        public CheckersRulesPrimitiveTest()
        {
            _emptyAvailableMoves = new Cell[] { new Cell(), new Cell(), new Cell(), new Cell() };
            _rules = new CheckersRules();
        }

        [Fact]
        public void CheckersRulesPrimitiveTest_1()
        {
            var board = BoardMinifiedTest.CreateSampleBoard();
            board.Player1PiecesCount--;

            var piece = (PieceMinified)board.Player1Pieces[0];
            piece.IsCaptured = true;

            board.Player1Pieces[0] = piece;
            board = _rules.FastForwardAvailableMoves(board);

            var afterRulesAppending1 = (PieceMinified)board.Player1Pieces[0];
            afterRulesAppending1.HasAvailableMoves().Should().BeFalse();

            var afterRulesAppending2 = (PieceMinified)board.Player1Pieces[1];
            afterRulesAppending2.GetAvailableMoves()
                .Should()
                .BeEquivalentTo(new Cell[] { new Cell(2, 1), new Cell(), new Cell(), new Cell() });
        }

        [Fact]
        public void CheckersRulesPrimitiveTest_2()
        {
            var board = BoardMinifiedTest.CreateSampleBoard();
            board.Player1PiecesCount--;

            var pieceToCapture = new PieceMinified(1, 2, false, false, true);
            board.Pieces[1, 2] = new BoardCell(1, false);

            board.Player2Pieces[1] = pieceToCapture;

            board = _rules.FastForwardAvailableMoves(board);

            var afterRulesAppending1 = (PieceMinified)board.Player1Pieces[0];
            afterRulesAppending1.GetAvailableMoves()
                .Should()
                .BeEquivalentTo(new Cell[] { new Cell(2, 1), new Cell(), new Cell(), new Cell() });

            var afterRulesAppending2 = (PieceMinified)board.Player1Pieces[1];
            afterRulesAppending2.HasAvailableMoves().Should().BeFalse();
        }
    }
}
