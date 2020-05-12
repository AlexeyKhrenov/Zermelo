using Checkers;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using FluentAssertions;
using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.GameTreeSearch
{
    
    public unsafe class StateTransitionsTest
    {
        private StateTransitions _stateTransitions;

        public StateTransitionsTest()
        {
            _stateTransitions = new StateTransitions(new CheckersRules());
        }

        [Fact]
        public void StateTransitionsTest_1()
        {
            // ARRANGE
            var sourceBoardStr = new string[]
            {
                "____",
                "____",
                "__b_",
                "_w__"
            };
            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);
            sourceBoard.SwitchPlayers();

            var targetBoardStr = new string[]
            {
                "____",
                "____",
                "__b_",
                "_w__"
            };
            var targetBoard = new BoardMock(targetBoardStr, 4, false);

            var practiceBoardSource = sourceBoard.ToMinified();
            practiceBoardSource.ActivePlayer = true;

            var practiceBoardTarget = targetBoard.ToMinified();

            var gameNode = new GameNode();
            gameNode.Move = new HistoryItemMinified(new Cell(1, 3), new Cell(3, 1), true);
            practiceBoardSource = _stateTransitions.GoDown(practiceBoardSource, gameNode);
            practiceBoardSource = _stateTransitions.GoUp(practiceBoardSource, gameNode);

            practiceBoardSource.ClearMoves();

            var pieceSource = (PieceMinified)practiceBoardSource.Player1Pieces[0];
            var pieceTarget = (PieceMinified)practiceBoardTarget.Player1Pieces[0];

            pieceSource.Should().BeEquivalentTo(pieceTarget);
            pieceSource.Should().BeEquivalentTo(pieceTarget);
        }

        [Fact]
        public void StateTransitionsTest_2()
        {
            // todo - rewrite or remove this test 
            //var sourceBoardStr = new string[]
            //{
            //    "____",
            //    "____",
            //    "__b_",
            //    "_w__"
            //};
            //var sourceBoard = new BoardMock(sourceBoardStr, 4, false).ToMinified();

            //var targetBoard = _stateTransitions.Copy(sourceBoard);

            //var sourcePiece

            //sourceBoard.Player1Pieces[0].GetAvailableMoves()[0] = new Cell(0, 2);
            //sourceBoard.Player2Pieces[0].X = 0;
            //sourceBoard.Pieces[0, 0] = new BoardCell(0, false);

            //Assert.False(ReferenceEquals(targetBoard.Player2Pieces[0], sourceBoard.Player2Pieces[0]));
            //Assert.False(ReferenceEquals(targetBoard.Player2Pieces, sourceBoard.Player2Pieces));
            //targetBoard.Pieces[0, 0].Should().NotBeEquivalentTo(new BoardCell(0, false));
            //targetBoard.Player2Pieces[0].X.Should().NotBe(0);
            //targetBoard.Player1Pieces[0].GetAvailableMoves()[0].Should().NotBeEquivalentTo(new Cell(0, 2));
        }
    }
}
