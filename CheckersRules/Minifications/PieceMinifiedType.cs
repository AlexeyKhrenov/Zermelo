using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    [Flags]
    internal enum PieceMinifiedType
    {
        White = 1,
        Black = 2,
        Queen = 4,
        Captured = 8,
        CanGoDown = 16,
        CanGoUp = 32,
    }
}
