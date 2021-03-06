﻿using System.Collections.Generic;
using Game.Primitives;
using Game.PublicInterfaces;

namespace Checkers
{
    internal class Piece : IFigure
    {
        public Piece(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown)
        {
            X = x;
            Y = y;
            IsWhite = isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
            AvailableMoves = new List<Cell>();
        }

        public Piece(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown, bool isQueen)
        {
            X = x;
            Y = y;
            IsWhite = isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
            AvailableMoves = new List<Cell>();
            IsQueen = isQueen;
        }

        public bool CanGoUp { get; set; }

        public bool CanGoDown { get; set; }

        public bool IsWhite { get; set; }

        public bool IsQueen { get; set; }
        public byte X { get; set; }

        public byte Y { get; set; }

        public List<Cell> AvailableMoves { get; set; }

        public bool IsCaptured { get; set; }

        public string Type
        {
            get
            {
                if (IsWhite)
                {
                    if (IsQueen) return PieceTypes.WhiteQueen.ToString();

                    return PieceTypes.White.ToString();
                }

                if (IsQueen) return PieceTypes.BlackQueen.ToString();

                return PieceTypes.Black.ToString();
            }
        }

        public bool Equals(IFigure other)
        {
            if (other == null) return false;

            return X == other.X && Y == other.Y && Type == other.Type;
        }

        // todo - consider changing to smaller value types
        public override int GetHashCode()
        {
            return (X << 8) + Y;
        }
    }
}