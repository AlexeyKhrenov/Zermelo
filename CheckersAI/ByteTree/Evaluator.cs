using System.Runtime.CompilerServices;

namespace CheckersAI.ByteTree
{
    internal class Evaluator : TreeSearch.IEvaluator<sbyte, sbyte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sbyte Evaluate(sbyte state)
        {
            return state;
        }
    }
}
