using System.Runtime.CompilerServices;

namespace CheckersAI.ByteTree
{
    internal class Comparator :
        CheckersAI.TreeSearch.IComparator<sbyte>,
        CheckersAI.MultithreadedTreeSearch.IComparator<sbyte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBigger(sbyte a, sbyte b)
        {
            return a > b;
        }
    }
}
