using Checkers;
using Game.Implementations;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            var sourceBoardStr = new string[]
            {
                "b_b_",
                "____",
                "____",
                "w_w_"
            };

            var sourceBoard = new BoardMock(sourceBoardStr, 4, false);

            var targetBoardStr = new string[]
            {
                "b_b_",
                "____",
                "___w",
                "w___"
            };

            var move = new HistoryItem(boardMock.ActivePlayer, new Point(2, 3), new Point(3, 2));

            
        }
    }
}
