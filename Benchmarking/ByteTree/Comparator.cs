using System.Runtime.CompilerServices;

namespace Benchmarking.ByteTree
{
    internal class Comparator :
        CheckersAI.TreeSearch.IComparator<byte>,
        CheckersAI.AsyncTreeSearch.IComparator<byte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBigger(byte a, byte b)
        {
            return a > b;
        }
    }
}
