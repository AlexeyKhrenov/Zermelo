using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Checkers.Minifications
{
    public class PieceMinified : IMementoMinification<Piece>
    {
        public int X;
        public int Y;

        public List<Point> AvailableMoves { get; set; }

        public bool CanGoUp { get; set; }

        public bool CanGoDown { get; set; }

        public bool IsWhite { get; set; }

        public bool IsQueen { get; set; }

        public void Maximize(Piece to)
        {
            to.X = X;
            to.Y = Y;
            to.CanGoDown = CanGoDown;
            to.CanGoUp = CanGoUp;
            to.IsWhite = IsWhite;
            to.IsQueen = IsQueen;
        }

        public Piece ToPiece()
        {
            var piece = new Piece(X, Y, IsWhite, CanGoUp, CanGoDown);
            piece.IsQueen = IsQueen;
            piece.AvailableMoves = AvailableMoves;
            return piece;
        }

        public PieceMinified()
        {
            AvailableMoves = new List<Point>();
        }

        public PieceMinified(int x, int y, bool isWhite, bool canGoUp, bool canGoDown)
        {
            AvailableMoves = new List<Point>();

            X = x;
            Y = y;
            IsWhite = isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
        }

        public void Minify(Piece from)
        {
            X = from.X;
            Y = from.Y;
            CanGoDown = from.CanGoDown;
            CanGoUp = from.CanGoUp;
            IsWhite = from.IsWhite;
            IsQueen = from.IsQueen;
        }
    }
}
