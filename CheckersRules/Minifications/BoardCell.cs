using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    public struct BoardCell
    {
        /// <summary>
        /// 000000 01 stands for white
        /// 000000 10 stands for black
        /// another 6 digits represent index of PieceMinified inside array of Player pieces
        /// </summary>
        private byte _value;

        public bool IsEmpty()
        {
            return _value == 0;
        }

        public BoardCell(byte index, bool isWhite)
        {
            _value = (byte)((byte)(index << 2) + (isWhite ? 1 : 2));
        }

        public BoardCell(byte value)
        {
            _value = value;
        }

        public bool IsWhite()
        {
            return !IsEmpty() && _value % 2 == 1;
        }

        public byte GetIndex()
        {
            return (byte)(_value >> 2);
        }

        public void RemovePiece()
        {
            _value = 0;
        }

        public static implicit operator byte(BoardCell p) => p._value;
        public static explicit operator BoardCell(byte b) => new BoardCell(b);
    }
}
