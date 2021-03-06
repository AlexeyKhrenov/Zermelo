﻿using Checkers.Minifications;
using FluentAssertions;
using Xunit;

namespace ZermeloUnitTests.PrimitivesMinifications
{
    public unsafe class BoardMinifiedTest
    {
        internal static BoardMinified CreateSampleBoard()
        {
            var board = new BoardMinified(4);

            board.Player1Pieces[0] = new PieceMinified(0, 3, true, true, false, false);
            board.Player1Pieces[1] = new PieceMinified(3, 2, true, true, false, false);
            board.Player2Pieces[0] = new PieceMinified(1, 0, true, true, false, false);

            board.SetBoardCell(1, 0, new BoardCell(0, false));
            board.SetBoardCell(0, 3, new BoardCell(0, true));
            board.SetBoardCell(3, 2, new BoardCell(1, true));

            board.Player1PiecesCount = 2;
            board.Player2PiecesCount = 1;

            return board;
        }

        [Fact]
        public void BoardMinifiedTest_1()
        {
            var board = CreateSampleBoard();
            board.RemovePiece(0, 3, true);

            board.Player1PiecesCount.Should().Be(1);
            board.Player2PiecesCount.Should().Be(1);

            var piece = (PieceMinified) board.Player1Pieces[0];
            piece.IsCaptured.Should().BeTrue();
        }

        [Fact]
        public void BoardMinifiedTest_2()
        {
            var board = CreateSampleBoard();
            board.MovePiece(0, 3, 0, 2, true);

            board.Player1PiecesCount.Should().Be(2);
            board.Player2PiecesCount.Should().Be(1);

            var piece = (PieceMinified) board.Player1Pieces[0];
            piece.X.Should().Be(0);
            piece.Y.Should().Be(2);
        }

        [Fact]
        public void BoardMinifiedTest_3()
        {
            var board = CreateSampleBoard();
            board.GetPlayer1PiecesList();
            board.RemovePiece(0, 3, true);
            board.RemovePiece(3, 2, true);
            board.RestorePiece(new PieceMinified(0, 3, true, true, false), true);

            board.Player1PiecesCount.Should().Be(1);
            board.Player2PiecesCount.Should().Be(1);
        }

        [Fact]
        public void BoardMinifiedTest_4()
        {
            var board = CreateSampleBoard();

            board.Player1Pieces[0] = new PieceMinified(1, 2, true, true, false, false);
            board.Player1Pieces[1] = new PieceMinified(2, 3, true, true, false, false);

            board.SetBoardCell(1, 2, new BoardCell(0, true));
            board.SetBoardCell(2, 3, new BoardCell(1, true));

            board.RemovePiece(1, 2, true);
            board.MovePiece(2, 3, 1, 2, true);

            board.MovePiece(1, 2, 2, 3, true);
            board.RestorePiece(new PieceMinified(1, 2, true, true, false), true);

            board.Player1PiecesCount.Should().Be(2);
        }
    }
}