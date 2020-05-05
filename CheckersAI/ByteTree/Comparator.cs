using CheckersAI.InternalInterfaces;
using System.Runtime.CompilerServices;

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
