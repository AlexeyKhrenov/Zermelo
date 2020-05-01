using System.Runtime.CompilerServices;

namespace CheckersAI.CheckersGameTree
{
    internal class Comparator : TreeSearch.IComparator<sbyte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBigger(sbyte a, sbyte b)
        {
            return a > b;
        }
    }
}
