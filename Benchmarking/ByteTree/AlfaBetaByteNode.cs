namespace Benchmarking.ByteTree
{
    public class AlfaBetaByteNode : CheckersAI.AsyncTreeSearch.IAlfaBetaNode<AlfaBetaByteNode, byte, byte>
    {
        public byte Alfa { get; set; }

        public byte Beta { get; set; }

        public byte Value { get; set; }

        public AlfaBetaByteNode[] Children { get; set; }

        public AlfaBetaByteNode Parent { get; set; }

        public AlfaBetaByteNode BestChild { get; set; }

        public bool IsMaxPlayer { get; set; }
        public AlfaBetaByteNode BestMove { get; set; }

        public AlfaBetaByteNode(byte value, bool isMaxPlayer, params AlfaBetaByteNode[] children)
        {
            Value = value;
            IsMaxPlayer = isMaxPlayer;
            Children = children;

            Alfa = byte.MinValue;
            Beta = byte.MaxValue;
        }

        public void LinkBackChildren()
        {
            foreach (var child in Children)
            {
                child.Parent = this;
            }
        }
    }
}
