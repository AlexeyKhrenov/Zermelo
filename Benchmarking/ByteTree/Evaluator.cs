using System.Threading.Tasks;

namespace Benchmarking.ByteTree
{
    internal class Evaluator :
        CheckersAI.TreeSearch.IEvaluator<ByteNode, byte, byte>,
        CheckersAI.AsyncTreeSearch.IEvaluator<AlfaBetaByteNode, byte, byte>
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Evaluate(ByteNode node)
        {
            return node.Value;
        }

        public async Task<byte> Evaluate(AlfaBetaByteNode node)
        {
            return await Task.FromResult(node.Value);
        }
    }
}
