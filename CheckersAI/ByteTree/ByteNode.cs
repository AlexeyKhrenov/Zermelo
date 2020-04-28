using CheckersAI.TreeSearch;

namespace CheckersAI.ByteTree
{
    internal class ByteNode : INode<ByteNode, sbyte>
    {
        public sbyte Value { get; set; }

        public ByteNode[] Children { get; set; }

        public bool IsMaxPlayer { get; set; }

        public ByteNode(sbyte value, bool isMaxPlayer, params ByteNode[] children)
        {
            Value = value;
            IsMaxPlayer = isMaxPlayer;
            Children = children;
        }
    }
}
