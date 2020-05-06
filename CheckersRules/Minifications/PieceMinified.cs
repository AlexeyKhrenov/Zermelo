using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    internal class PieceMinified
    {
        public byte X;
        public byte Y;

        public Cell[] AvailableMoves { get; set; }

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

        public PieceMinified(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown)
        {
            AvailableMoves = new Cell[4];

            X = x;
            Y = y;
            IsWhite = isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
        }

        public PieceMinified(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown, bool isQueen)
        {
            AvailableMoves = new Cell[4];

            X = x;
            Y = y;
            IsWhite = isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
            IsQueen = isQueen;
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
    }
}
