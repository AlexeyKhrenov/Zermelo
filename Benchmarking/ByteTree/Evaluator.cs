using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Benchmarking.ByteTree
{
    internal class Evaluator :
        CheckersAI.TreeSearch.IEvaluator<ByteNode, byte, byte>,
        CheckersAI.MultithreadedTreeSearch.IEvaluator<AlfaBetaByteNode, byte, byte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Evaluate(ByteNode node)
        {
            return node.Value;
        }

        public byte Evaluate(AlfaBetaByteNode node)
        {
            return node.Value;
        }
    }
}
