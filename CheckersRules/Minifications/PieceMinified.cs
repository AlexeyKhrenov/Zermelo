using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Game.Primitives;

namespace Checkers.Minifications
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct PieceMinified : IEquatable<PieceMinified>
    {
        [FieldOffset(0)] public int Value;

        [FieldOffset(0)] public byte X;
        [FieldOffset(1)] public byte Y;
        [FieldOffset(2)] private byte _type;
        [FieldOffset(3)] private AvailableMoves _availableMoves;

        public bool CanGoDown
        {
            get => (_type & (byte) PieceMinifiedType.CanGoDown) != 0;
            set
            {
                if (value)
                    _type = (byte) (_type | (byte) PieceMinifiedType.CanGoDown);
                else
                    _type = (byte) (_type & ~(byte) PieceMinifiedType.CanGoDown);
            }
        }

        public bool IsCaptured
        {
            get => (_type & (byte) PieceMinifiedType.Captured) != 0;
            set
            {
                if (value)
                    _type = (byte) (_type | (byte) PieceMinifiedType.Captured);
                else
                    _type = (byte) (_type & ~(byte) PieceMinifiedType.Captured);
            }
        }

        public bool CanGoUp
        {
            get => (_type & (byte) PieceMinifiedType.CanGoUp) != 0;
            set
            {
                if (value)
                    _type = (byte) (_type | (byte) PieceMinifiedType.CanGoUp);
                else
                    _type = (byte) (_type & ~(byte) PieceMinifiedType.CanGoUp);
            }
        }

        public bool IsWhite
        {
            get => (_type & (byte) PieceMinifiedType.White) != 0;
            set
            {
                if (value)
                    _type = (byte) (_type | (byte) PieceMinifiedType.White);
                else
                    _type = (byte) (_type & ~(byte) PieceMinifiedType.White);
            }
        }

        public bool IsQueen
        {
            get => (_type & (byte) PieceMinifiedType.Queen) != 0;
            set
            {
                if (value)
                    _type = (byte) (_type | (byte) PieceMinifiedType.Queen);
                else
                    _type = (byte) (_type & ~(byte) PieceMinifiedType.Queen);
            }
        }

        public bool IsBlack
        {
            get => (_type & (byte) PieceMinifiedType.Black) != 0;
            set
            {
                if (value)
                    _type = (byte) (_type | (byte) PieceMinifiedType.Black);
                else
                    _type = (byte) (_type & ~(byte) PieceMinifiedType.Black);
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
            Value = 0;
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
            Value = 0;
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

        public PieceMinified(int value) : this()
        {
            Value = value;
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
            return ((other._type ^ _type) & 15) == 0 && other.X == X && other.Y == Y;
        }

        public bool IsEmpty()
        {
            return _type == 0;
        }

        public static implicit operator int(PieceMinified p)
        {
            return p.Value;
        }

        public static explicit operator PieceMinified(int i)
        {
            return Unsafe.As<int, PieceMinified>(ref i);
        }
    }
}