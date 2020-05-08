using Checkers.Minifications;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZermeloUnitTests.Mocks;

namespace ZermeloUnitTests.PrimitivesMinifications
{
    public class BoardMinifiedTest
    {
        [Fact]
        public void BoardMinifiedTest_1()
        {
            var board = CreateSampleBoard();
            board.RemovePiece(0, 3, true);

            board.Player1PiecesCount.Should().Be(1);
            board.Player2PiecesCount.Should().Be(1);
            board.Player1Pieces[0].IsCaptured.Should().BeTrue();
        }

        [Fact]
        public void BoardMinifiedTest_2()
        {
            var board = CreateSampleBoard();
            board.MovePiece(0, 3, 0, 2, true);

            board.Player1PiecesCount.Should().Be(2);
            board.Player2PiecesCount.Should().Be(1);
            board.Player1Pieces[0].X.Should().Be(0);
            board.Player1Pieces[0].Y.Should().Be(2);
        }

        [Fact]
        public void BoardMinifiedTest_3()
        {
            var board = CreateSampleBoard();
            board.RemovePiece(0, 3, true);
            board.RemovePiece(3, 2, true);
            board.RestorePiece(new PieceMinified(0, 3, true, true, false), true);

            board.Player1PiecesCount.Should().Be(1);
            board.Player2PiecesCount.Should().Be(1);
        }

        internal static BoardMinified CreateSampleBoard()
        {
            var board = new BoardMinified(4);

            board.Player1Pieces[0] = new PieceMinified(0, 3, true, true, false, false);
            board.Player1Pieces[1] = new PieceMinified(3, 2, true, true, false, false);
            board.Player2Pieces[0] = new PieceMinified(1, 0, true, true, false, false);

            board.Pieces[1, 0] = new BoardCell(0, false);
            board.Pieces[0, 3] = new BoardCell(0, true);
            board.Pieces[3, 2] = new BoardCell(1, true);

            board.Player1PiecesCount = 2;
            board.Player2PiecesCount = 1;

            return board;
        }
    }
}
