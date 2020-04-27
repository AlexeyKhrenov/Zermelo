using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    internal class PieceMinified : IMementoMinification<Piece>
    {
        public byte X;
        public byte Y;

        public List<Cell> AvailableMoves { get; set; }

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
            AvailableMoves = new List<Cell>();
        }

        public PieceMinified(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown)
        {
            AvailableMoves = new List<Cell>();

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
