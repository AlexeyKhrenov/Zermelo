using CheckersAI.InternalInterfaces;
using System.Runtime.CompilerServices;

namespace CheckersAI.ByteTree
{
    internal class Evaluator : IEvaluator<sbyte, sbyte>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sbyte Evaluate(sbyte state)
        {
            return state;
        }
    }
}
