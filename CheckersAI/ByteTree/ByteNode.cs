using System;
using CheckersAI.InternalInterfaces;

namespace CheckersAI.ByteTree
{
    internal class ByteNode : INode<ByteNode, sbyte>
    {
        public ByteNode(sbyte valueChange, bool isMaxPlayer, params ByteNode[] children)
        {
            ValueChange = valueChange;
            IsMaxPlayer = isMaxPlayer;
            Children = children;
        }

        public sbyte ValueChange { get; set; }
        public sbyte Result { get; set; }

        public ByteNode[] Children { get; set; }

        public bool IsMaxPlayer { get; set; }

        public ByteNode BestChild { get; set; }

        public void Clear()
        {
            Result = sbyte.MinValue;
            BestChild = null;
        }

        public bool Equals(ByteNode other)
        {
            throw new NotImplementedException();
        }

        // todo - implement
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}