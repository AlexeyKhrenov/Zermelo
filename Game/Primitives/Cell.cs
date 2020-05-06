using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Primitives
{
    public readonly struct Cell
    {
        public byte X { get; }
        public byte Y { get; }

        // todo - compare size with Nullable<Cell>s
        public bool IsNotNull { get; }

        public Cell(byte x, byte y)
        {
            X = x;
            Y = y;
            IsNotNull = true;
        }
    }
}
