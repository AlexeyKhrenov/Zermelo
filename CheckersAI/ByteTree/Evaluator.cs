using System.Runtime.CompilerServices;
using CheckersAI.InternalInterfaces;

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