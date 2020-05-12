using System.Runtime.CompilerServices;
using CheckersAI.InternalInterfaces;

namespace CheckersAI.ByteTree
{
    internal class Comparator : IComparator<sbyte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBigger(sbyte a, sbyte b)
        {
            return a > b;
        }
    }
}