using CheckersAI.InternalInterfaces;

namespace CheckersAI.ByteTree
{
    internal class StateTransitions : IStateTransitions<sbyte, ByteNode, sbyte>
    {
        public sbyte GoDown(sbyte state, ByteNode node)
        {
            return (sbyte)(state + node.ValueChange);
        }

        public sbyte GoUp(sbyte state, ByteNode node)
        {
            return (sbyte)(state - node.ValueChange);
        }
    }
}
