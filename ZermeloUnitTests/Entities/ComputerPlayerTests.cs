using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CheckersAI;
using FluentAssertions;
using Game.Primitives;
using Game.PublicInterfaces;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.Entities
{
    public class ComputerPlayerTests
    {
        public ComputerPlayerTests()
        {
            _wrapper = new ProgressiveWrapperMock();
            _treeManager = new TreeManagerMock();
        }

        private readonly TreeManagerMock _treeManager;
        private readonly ProgressiveWrapperMock _wrapper;

        /// <summary>
        ///     computer should do two moves
        /// </summary>
        [Fact]
        public void ComputerPlayerTest_1()
        {
            var computerPlayer = new ComputerPlayer("P1", _wrapper, _treeManager);
            var anotherPlayer = new PlayerMock("P2");

            var board1Str = new[]
            {
                "_____",
                "_b___",
                "_____",
                "_b___",
                "w____"
            };
            var board1 = new BoardMock(board1Str, 5, false);
            board1.ActivePlayer.Figures
                .First(f => f.X == 0 && f.Y == 4)
                .AvailableMoves = new List<Cell> {new Cell(1, 3)};

            var board2Str = new[]
            {
                "_____",
                "_b___",
                "__w__",
                "_____",
                "_____"
            };
            var board2 = new BoardMock(board2Str, 5, false);
            board2.ActivePlayer.Figures
                .First(f => f.X == 2 && f.Y == 2)
                .AvailableMoves = new List<Cell> {new Cell(0, 0)};

            var board3Str = new[]
            {
                "W____",
                "_____",
                "_____",
                "_____",
                "_____"
            };
            var board3 = new BoardMock(board3Str, 5, false);
            board3.SwitchPlayers();

            var gameMock = new GameMock(
                new[] {board1, board2, board3},
                new IPlayer[] {computerPlayer, computerPlayer, anotherPlayer});

            var cts = new CancellationTokenSource();
            computerPlayer.MakeMove(gameMock, cts.Token).Wait();

            gameMock.Moves
                .Should()
                .BeEquivalentTo(new[]
                {
                    new Move(0, 4, 1, 3),
                    new Move(2, 2, 0, 0)
                });
        }
    }
}