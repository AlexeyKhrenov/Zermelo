using System.Runtime.CompilerServices;

namespace CheckersAI.ByteTree
{
    internal class Evaluator : TreeSearch.IEvaluator<ByteNode, sbyte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sbyte Evaluate(ByteNode node)
        {
            return node.Value;
        }
    }
}
