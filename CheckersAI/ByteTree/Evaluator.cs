using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CheckersAI.ByteTree
{
    internal class Evaluator :
        CheckersAI.TreeSearch.IEvaluator<ByteNode, sbyte, sbyte>,
        CheckersAI.MultithreadedTreeSearch.IEvaluator<AlfaBetaByteNode, sbyte, sbyte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sbyte Evaluate(ByteNode node)
        {
            return node.Value;
        }

        public sbyte Evaluate(AlfaBetaByteNode node)
        {
            return node.Value;
        }
    }
}
