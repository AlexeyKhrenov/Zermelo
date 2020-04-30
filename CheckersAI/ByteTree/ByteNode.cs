using CheckersAI.TreeSearch;

namespace CheckersAI.ByteTree
{
    internal class ByteNode : INode<ByteNode>
    {
        public sbyte ValueChange { get; set; }

        public ByteNode[] Children { get; set; }

        public bool IsMaxPlayer { get; set; }

        public ByteNode(sbyte valueChange, bool isMaxPlayer, params ByteNode[] children)
        {
            ValueChange = valueChange;
            IsMaxPlayer = isMaxPlayer;
            Children = children;
        }
    }
}
