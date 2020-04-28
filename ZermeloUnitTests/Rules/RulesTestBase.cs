﻿using Checkers;
using FluentAssertions;
using Game.PublicInterfaces;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.Rules
{
    public class RulesTestBase
    {
        // todo - do we really need rules have size?
        protected IGameRules _rules = new CheckersRules(4);

        protected void AssertBoardsAreEqual(BoardMock targetBoard, BoardMock sourceBoard)
        {
            targetBoard.Player1.Figures
                .Should()
                .BeEquivalentTo(sourceBoard.Player1.Figures);

            targetBoard.Player2.Figures
                .Should()
                .BeEquivalentTo(sourceBoard.Player2.Figures);

            targetBoard.ActivePlayer.Name.Should().BeEquivalentTo(sourceBoard.ActivePlayer.Name);
        }
    }
}