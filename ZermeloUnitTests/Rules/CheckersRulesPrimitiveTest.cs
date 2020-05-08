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
            board.Player1Pieces[0].IsCaptured = true;
            _rules.FastForwardAvailableMoves(board);

            board.Player1Pieces[0].AvailableMoves
                .Should()
                .BeEquivalentTo(_emptyAvailableMoves);

            board.Player2Pieces[0].AvailableMoves
                .Should()
                .BeEquivalentTo(_emptyAvailableMoves);

            board.Player1Pieces[1].AvailableMoves
                .Should()
                .BeEquivalentTo(new Cell[] { new Cell(2, 1), new Cell(), new Cell(), new Cell() });
        }
    }
}
