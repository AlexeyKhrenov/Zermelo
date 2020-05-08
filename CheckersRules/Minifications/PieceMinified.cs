using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    internal struct PieceMinified : IEquatable<PieceMinified>
    {
        public byte X;
        public byte Y;

        public bool IsCaptured { get; set; }

        public Cell[] AvailableMoves { get; set; }

        public bool CanGoUp { get; set; }

        public bool CanGoDown { get; set; }

        public bool IsWhite { get; set; }

        public bool IsBlack { get; set; }

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

        public PieceMinified(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown)
        {
            AvailableMoves = new Cell[4];

            X = x;
            Y = y;
            IsWhite = isWhite;
            IsBlack = !isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
            IsCaptured = false;
            IsQueen = false;
        }

        public PieceMinified(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown, bool isQueen)
        {
            AvailableMoves = new Cell[4];

            X = x;
            Y = y;
            IsWhite = isWhite;
            IsBlack = !isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
            IsQueen = isQueen;
            IsCaptured = false;
        }

        public void ClearMoves()
        {
            AvailableMoves = new Cell[4];
        }

        public byte CountAvailableMoves()
        {
            byte result = 0;
            foreach (var cell in AvailableMoves)
            {
                if (cell.IsNotNull)
                {
                    result++;
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public bool Equals(PieceMinified other)
        {
            return
                other.IsWhite == IsWhite &&
                other.IsBlack == IsBlack &&
                other.IsQueen == IsQueen &&
                other.X == X &&
                other.Y == Y;
        }

        public bool IsEmpty()
        {
            return !IsWhite && !IsBlack;
        }
    }
}
