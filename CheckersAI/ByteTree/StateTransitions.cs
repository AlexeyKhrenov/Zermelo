using CheckersAI.InternalInterfaces;

namespace CheckersAI.ByteTree
{
    internal class StateTransitions : IStateTransitions<sbyte, ByteNode, sbyte>,
        IStateTransitions<sbyte, AlfaBetaByteNode, sbyte>
    {
        public sbyte GoDown(sbyte state, AlfaBetaByteNode node)
        {
            return (sbyte) (state + node.ValueChange);
        }

        public sbyte GoUp(sbyte state, AlfaBetaByteNode node)
        {
            return (sbyte) (state - node.ValueChange);
        }

        public sbyte GoDown(sbyte state, ByteNode node)
        {
            return (sbyte) (state + node.ValueChange);
        }

        public sbyte GoUp(sbyte state, ByteNode node)
        {
            return (sbyte) (state - node.ValueChange);
        }

        public sbyte Copy(sbyte state)
        {
            return state;
        }

        public void DeallocateCopy(sbyte state)
        {
        }
    }
}