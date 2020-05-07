using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Minifications
{
    internal static class MinificationsHelper
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

            // todo - remove this cast
            for (var i = 0; i < from.AvailableMoves.Count; i++)
            {
                min.AvailableMoves[i] = from.AvailableMoves[i];
            }

            return min;
        }

        public static Piece ToMaximized(this PieceMinified min)
        {
            var piece = new Piece(min.X, min.Y, min.IsWhite, min.CanGoUp, min.CanGoDown);
            piece.IsQueen = min.IsQueen;
            piece.IsCaptured = min.IsCaptured;
            piece.AvailableMoves = min.AvailableMoves.Where(x => x.IsNotNull).ToList();
            return piece;
        }

        public static BoardMinified ToMinified(this IBoard from)
        {
            var min = new BoardMinified();
            min.Player1Pieces = new List<PieceMinified>();
            min.Player2Pieces = new List<PieceMinified>();

            min.Pieces = new BoardCell[from.Size, from.Size];
            min.InvertedCoordinates = from.InvertedCoordinates;

            min.ActivePlayer = from.ActivePlayer == from.Player1;

            for (byte i = 0; i < from.Player1.Figures.Count; i++)
            {
                var minPiece = ((Piece)from.Player1.Figures[i]).ToMinified();
                min.Player1Pieces.Add(minPiece);

                if (!minPiece.IsCaptured)
                {
                    min.Pieces[minPiece.X, minPiece.Y] = new BoardCell(i, minPiece.IsWhite);
                }
            }

            for (byte i = 0; i < from.Player2.Figures.Count; i++)
            {
                var minPiece = ((Piece)from.Player2.Figures[i]).ToMinified();
                min.Player2Pieces.Add(minPiece);

                if (!minPiece.IsCaptured)
                {
                    min.Pieces[minPiece.X, minPiece.Y] = new BoardCell(i, minPiece.IsWhite);
                }
            }

            min.Player1PiecesCount = (byte)min.Player1Pieces.Count;
            min.Player2PiecesCount = (byte)min.Player2Pieces.Count;

            return min;
        }

        public static void ToMaximized(this BoardMinified from, IBoard to)
        {
            to.Player1.Figures = from.Player1Pieces.Select(x => (IFigure)x.ToMaximized()).ToList();
            to.Player2.Figures = from.Player2Pieces.Select(x => (IFigure)x.ToMaximized()).ToList();

            if (to.ActivePlayer != (from.ActivePlayer ? to.Player1 : to.Player2))
            {
                to.SwitchPlayers();      
            }

            to.ActivePlayer.IsActive = true;
            to.AwaitingPlayer.IsActive = false;
        }
    }
}
