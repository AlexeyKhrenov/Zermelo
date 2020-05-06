using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
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
            min.AvailableMoves = from.AvailableMoves;

            return min;
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
    }
}
