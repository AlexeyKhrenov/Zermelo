using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Primitives
{
    public readonly struct Cell
    {
        public Cell(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public byte X { get; }
        public byte Y { get; }
    }
}
