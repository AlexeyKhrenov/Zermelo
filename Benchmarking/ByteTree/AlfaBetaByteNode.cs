using System.Collections.Generic;

namespace Benchmarking.ByteTree
{
    public class AlfaBetaByteNode : CheckersAI.AsyncTreeSearch.IAlfaBetaNode<AlfaBetaByteNode, byte, byte>
    {
        public byte Alfa { get; set; }

        public byte Beta { get; set; }

        public byte Value { get; set; }

        public AlfaBetaByteNode[] Children { get; set; }

        public AlfaBetaByteNode Parent { get; set; }

        public bool IsMaxPlayer { get; set; }

        public AlfaBetaByteNode BestMove { get; set; }

        public bool IsFinalizedDuringSearch { get; set; }

        public byte ChildrenPropagatedCount { get; set; }

        public AlfaBetaByteNode(byte value, bool isMaxPlayer, params AlfaBetaByteNode[] children)
        {
            Value = value;
            IsMaxPlayer = isMaxPlayer;
            Children = children;

            Alfa = byte.MinValue;
            Beta = byte.MaxValue;

            IsFinalizedDuringSearch = false;
            ChildrenPropagatedCount = 0;
        }

        public void LinkBackChildren()
        {
            foreach (var child in Children)
            {
                child.Parent = this;
            }
        }

        public int GetDepth()
        {
            if (Children == null || Children.Length == 0)
            {
                return 0;
            }
            return Children[0].GetDepth() + 1;
        }

        public List<AlfaBetaByteNode> ToList()
        {
            var list = new List<AlfaBetaByteNode>();
            var level = new Queue<AlfaBetaByteNode>();

            level.Enqueue(this);

            while (level.TryDequeue(out var nextNode))
            {
                list.Add(nextNode);
                if (nextNode.Children != null)
                {
                    foreach (var child in nextNode.Children)
                    {
                        level.Enqueue(child);
                    }
                }
            }

            return list;
        }
    }
}
