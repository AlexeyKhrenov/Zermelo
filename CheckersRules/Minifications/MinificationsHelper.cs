using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Minifications
{
    internal unsafe static class MinificationsHelper
    {
        public static PieceMinified ToMinified(this Piece from)
        {
            var min = new PieceMinified(
                from.X,
                from.Y,
                from.IsWhite,
                from.CanGoUp,
                from.CanGoDown,
                from.IsQueen);

            min.IsCaptured = from.IsCaptured;

            return min;
        }

        public static Piece ToMaximized(this PieceMinified min)
        {
            var piece = new Piece(min.X, min.Y, min.IsWhite, min.CanGoUp, min.CanGoDown);
            piece.IsQueen = min.IsQueen;
            piece.IsCaptured = min.IsCaptured;
            piece.AvailableMoves = min.GetAvailableMoves().Where(x => x.IsNotNull).ToList();
            return piece;
        }

        public static BoardMinified ToMinified(this IBoard from)
        {
            var min = new BoardMinified(from.Size);

            min.ActivePlayer = from.ActivePlayer == from.Player1;

            // todo - change to one loop
            for (byte i = 0; i < from.Player1.Figures.Count; i++)
            {
                var minPiece = ((Piece)from.Player1.Figures[i]).ToMinified();
                min.Player1Pieces[i] = minPiece;

                if (!minPiece.IsCaptured)
                {
                    min.Pieces[minPiece.X, minPiece.Y] = new BoardCell(i, minPiece.IsWhite);
                }
            }

            for (byte i = 0; i < from.Player2.Figures.Count; i++)
            {
                var minPiece = ((Piece)from.Player2.Figures[i]).ToMinified();
                min.Player2Pieces[i] = minPiece;

                if (!minPiece.IsCaptured)
                {
                    min.Pieces[minPiece.X, minPiece.Y] = new BoardCell(i, minPiece.IsWhite);
                }
            }

            min.Player1PiecesCount = (byte)from.Player1.Figures.Count;
            min.Player2PiecesCount = (byte)from.Player2.Figures.Count;

            return min;
        }

        public static void ToMaximized(this BoardMinified from, IBoard to)
        {
            to.Player1.Figures.Clear();
            to.Player2.Figures.Clear();

            foreach (var piece in from.GetPlayer1PiecesList())
            {
                to.Player1.Figures.Add(piece.ToMaximized());
            }

            foreach (var piece in from.GetPlayer2PiecesList())
            {
                to.Player2.Figures.Add(piece.ToMaximized());
            }

            if (to.ActivePlayer != (from.ActivePlayer ? to.Player1 : to.Player2))
            {
                to.SwitchPlayers();
            }

            to.ActivePlayer.IsActive = true;
            to.AwaitingPlayer.IsActive = false;
        }
    }
}
