using Checkers;
using FluentAssertions;
using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZermeloUnitTests.PrimitivesMinifications;

namespace ZermeloUnitTests.Rules
{
    public class CheckersRulesPrimitiveTest
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
            board.Player1Pieces[0].IsCaptured = true;
            _rules.FastForwardAvailableMoves(board);

            board.Player1Pieces[0].HasAvailableMoves().Should().BeFalse();

            board.Player2Pieces[0].HasAvailableMoves().Should().BeFalse();

            board.Player1Pieces[1].GetAvailableMoves()
                .Should()
                .BeEquivalentTo(new Cell[] { new Cell(2, 1), new Cell(), new Cell(), new Cell() });
        }
    }
}
