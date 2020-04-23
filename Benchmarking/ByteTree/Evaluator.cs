using CheckersAI.TreeSearch;

namespace Benchmarking.ByteTree
{
    internal class Evaluator : IEvaluator<ByteNode, byte, byte>
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Evaluate(ByteNode node)
        {
            return node.Value;
        }
    }
}
