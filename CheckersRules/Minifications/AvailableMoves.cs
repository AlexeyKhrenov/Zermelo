using System.Runtime.CompilerServices;
using Game.Primitives;

namespace Checkers.Minifications
{
    public struct AvailableMoves
    {
        private byte _value;

        /// <summary>
        ///     direction as on coordinates plane
        ///     direction right values more
        /// </summary>
        public void AddDirection(sbyte directionRight, sbyte directionDown, bool isCapture)
        {
            _value |= (byte) (1 << (directionRight + 1) << ((directionDown + 1) / 2));
            _value |= (byte) (isCapture ? 16 : 0); // todo - consider using faster bool for byte conversion
        }

        internal Cell[] ToCells(byte x0, byte y0)
        {
            var cells = new Cell[4];

            if (_value == 0) return cells;

            byte i = 0;
            var isCaptureCoeff = (_value & 16) == 16 ? (byte) 2 : (byte) 1;
            if ((_value & 8) != 0)
            {
                cells[i] = new Cell((byte) (x0 + isCaptureCoeff), (byte) (y0 + isCaptureCoeff));
                i++;
            }

            if ((_value & 4) != 0)
            {
                cells[i] = new Cell((byte) (x0 + isCaptureCoeff), (byte) (y0 - isCaptureCoeff));
                i++;
            }

            if ((_value & 2) != 0)
            {
                cells[i] = new Cell((byte) (x0 - isCaptureCoeff), (byte) (y0 + isCaptureCoeff));
                i++;
            }

            if ((_value & 1) != 0) cells[i] = new Cell((byte) (x0 - isCaptureCoeff), (byte) (y0 - isCaptureCoeff));
            return cells;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _value = 0;
        }

        public bool HasAvailableMoves()
        {
            return _value != 0;
        }
    }
}