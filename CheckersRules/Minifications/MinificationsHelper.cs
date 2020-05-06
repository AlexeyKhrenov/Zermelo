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
            piece.AvailableMoves = min.AvailableMoves.Where(x => x.IsNotNull).ToList();
            return piece;
        }

        public static BoardMinified ToMinified(this IBoard from)
        {
            var min = new BoardMinified();
            min.Player1Pieces = new List<PieceMinified>();
            min.Player2Pieces = new List<PieceMinified>();

            min.Pieces = new PieceMinified[from.Size, from.Size];
            min.InvertedCoordinates = from.InvertedCoordinates;

            min.ActivePlayer = from.ActivePlayer == from.Player1;

            foreach (var figure in from.Player1.Figures)
            {
                var minPiece = ((Piece)figure).ToMinified();

                min.Player1Pieces.Add(minPiece);
                min.Pieces[figure.X, figure.Y] = minPiece;
            }

            foreach (var figure in from.Player2.Figures)
            {
                var minPiece = ((Piece)figure).ToMinified();

                min.Player2Pieces.Add(minPiece);
                min.Pieces[figure.X, figure.Y] = minPiece;
            }

            return min;
        }

        public static void ToMaximized(this BoardMinified from, IBoard to)
        {
            to.Player1.Figures = from.Player1Pieces.Select(x => x.ToMaximized()).ToList();
            to.Player2.Figures = from.Player2Pieces.Select(x => x.ToMaximized()).ToList();

            if (to.ActivePlayer != (from.ActivePlayer ? to.Player1 : to.Player2))
            {
                to.SwitchPlayers();
            }

            to.ActivePlayer.IsActive = true;
            to.AwaitingPlayer.IsActive = false;
        }
    }
}
