using CheckersAI.InternalInterfaces;

namespace CheckersAI.ByteTree
{
    internal class ByteNode : INode<ByteNode, sbyte>
    {
        public sbyte Result { get; set; }

        public sbyte ValueChange { get; set; }

        public ByteNode[] Children { get; set; }

        public bool IsMaxPlayer { get; set; }

        public ByteNode BestChild { get; set; }

        public ByteNode(sbyte valueChange, bool isMaxPlayer, params ByteNode[] children)
        {
            ValueChange = valueChange;
            IsMaxPlayer = isMaxPlayer;
            Children = children;
        }

        // todo - implement
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Clear()
        {
            Result = sbyte.MinValue;
            BestChild = null;
        }

        public bool Equals(ByteNode other)
        {
            throw new System.NotImplementedException();
        }
    }
}
