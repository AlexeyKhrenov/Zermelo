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

        private byte _type;

        public bool CanGoDown
        {
            get { return (_type & (byte)PieceMinifiedType.CanGoDown) != 0; }
            set
            {
                if (value == true)
                {
                    _type = (byte)(_type | (byte)PieceMinifiedType.CanGoDown);
                }
                else
                {
                    _type = (byte)(_type & ~(byte)PieceMinifiedType.CanGoDown);
                }
            }
        }

        public bool IsCaptured
        {
            get { return (_type & (byte)PieceMinifiedType.Captured) != 0; }
            set
            {
                if (value == true)
                {
                    _type = (byte)(_type | (byte)PieceMinifiedType.Captured);
                }
                else
                {
                    _type = (byte)(_type & ~(byte)PieceMinifiedType.Captured);
                }
            }
        }

        public bool CanGoUp
        {
            get { return (_type & (byte)PieceMinifiedType.CanGoUp) != 0; }
            set
            {
                if (value == true)
                {
                    _type = (byte)(_type | (byte)PieceMinifiedType.CanGoUp);
                }
                else
                {
                    _type = (byte)(_type & ~(byte)PieceMinifiedType.CanGoUp);
                }
            }
        }

        public bool IsWhite
        {
            get { return (_type & (byte)PieceMinifiedType.White) != 0; }
            set
            {
                if (value == true)
                {
                    _type = (byte)(_type | (byte)PieceMinifiedType.White);
                }
                else
                {
                    _type = (byte)(_type & ~(byte)PieceMinifiedType.White);
                }
            }
        }

        public bool IsQueen
        {
            get { return (_type & (byte)PieceMinifiedType.Queen) != 0; }
            set
            {
                if (value == true)
                {
                    _type = (byte)(_type | (byte)PieceMinifiedType.Queen);
                }
                else
                {
                    _type = (byte)(_type & ~(byte)PieceMinifiedType.Queen);
                }
            }
        }

        public bool IsBlack
        {
            get { return (_type & (byte)PieceMinifiedType.Black) != 0; }
            set
            {
                if (value == true)
                {
                    _type = (byte)(_type | (byte)PieceMinifiedType.Black);
                }
                else
                {
                    _type = (byte)(_type & ~(byte)PieceMinifiedType.Black);
                }
            }
        }

        public Cell[] AvailableMoves { get; set; }

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
            _type = 0;

            IsBlack = !isWhite;
            IsQueen = false;
            IsWhite = isWhite;
            CanGoUp = canGoUp;
            CanGoDown = canGoDown;
            IsCaptured = false;
        }

        public PieceMinified(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown, bool isQueen)
        {
            AvailableMoves = new Cell[4];

            X = x;
            Y = y;
            _type = 0;

            IsBlack = !isWhite;
            IsQueen = isQueen;
            IsWhite = isWhite;
            CanGoUp = canGoUp;
            IsCaptured = false;
            CanGoDown = canGoDown;
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
            return (((other._type ^ _type) & 15) == 0) && other.X == X && other.Y == Y;
        }

        public bool IsEmpty()
        {
            return !IsWhite && !IsBlack;
        }
    }
}
