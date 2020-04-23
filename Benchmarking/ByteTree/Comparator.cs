using CheckersAI.TreeSearch;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Benchmarking.ByteTree
{
    internal class Comparator : IComparator<byte>
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBigger(byte a, byte b)
        {
            return a > b;
        }
    }
}
