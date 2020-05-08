﻿using Checkers;
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
    
    public class StateTransitionsTest
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

            practiceBoardSource.Player1Pieces[0].AvailableMoves = null;
            practiceBoardTarget.Player1Pieces[0].AvailableMoves = null;
            practiceBoardSource.Player2Pieces[0].AvailableMoves = null;
            practiceBoardTarget.Player2Pieces[0].AvailableMoves = null;

            practiceBoardSource.Player1Pieces[0].Should().BeEquivalentTo(practiceBoardTarget.Player1Pieces[0]);
            practiceBoardSource.Player2Pieces[0].Should().BeEquivalentTo(practiceBoardTarget.Player2Pieces[0]);
        }
        
    }
}