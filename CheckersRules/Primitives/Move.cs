using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Primitives
{
    public readonly struct Move
    {
        public Move(byte x0, byte y0, byte x1, byte y1)
        {
            X0 = x0;
            Y0 = y0;
            X1 = x1;
            Y1 = y1;
        }

        public byte X0 { get; }
        public byte X1 { get; }
        public byte Y0 { get; }
        public byte Y1 { get; }
    }
}
