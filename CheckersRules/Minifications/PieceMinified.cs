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

        private AvailableMoves _availableMoves;

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
            X = x;
            Y = y;
            _type = 0;
            _availableMoves = new AvailableMoves();

            IsBlack = !isWhite;
            IsQueen = false;
            IsWhite = isWhite;
            CanGoUp = canGoUp;
            CanGoDown = canGoDown;
            IsCaptured = false;
        }

        public void AddAvailableMove(sbyte directionRight, sbyte directionDown, bool isCapture)
        {
            _availableMoves.AddDirection(directionRight, directionDown, isCapture);
        }

        public void UpdateAvailableMoves(PieceMinified piece)
        {
            _availableMoves = piece._availableMoves;
        }

        public PieceMinified(byte x, byte y, bool isWhite, bool canGoUp, bool canGoDown, bool isQueen)
        {
            X = x;
            Y = y;
            _type = 0;
            _availableMoves = new AvailableMoves();

            IsBlack = !isWhite;
            IsQueen = isQueen;
            IsWhite = isWhite;
            CanGoUp = canGoUp;
            IsCaptured = false;
            CanGoDown = canGoDown;
        }

        public Cell[] GetAvailableMoves()
        {
            return _availableMoves.ToCells(X, Y);
        }

        public bool HasAvailableMoves()
        {
            return _availableMoves.HasAvailableMoves();
        }

        public void ClearMoves()
        {
            _availableMoves.Clear();
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
            return _type == 0;
        }
    }
}
