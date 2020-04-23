using CheckersAI.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking.ByteTree
{
    internal class ByteNode : INode<ByteNode, byte>
    {
        public byte Value { get; set; }

        public ByteNode[] Children { get; set; }

        public bool IsMaxPlayer { get; set; }

        public ByteNode(byte value, bool isMaxPlayer, params ByteNode[] children)
        {
            Value = value;
            IsMaxPlayer = isMaxPlayer;
            Children = children;
        }

        public void Branch(byte? branchingFactor = null)
        {
            if (branchingFactor != null)
            {
                Children = new ByteNode[branchingFactor.Value];
                for (var i = 0; i < Children.Length; i++)
                {
                    Children[i] = new ByteNode((byte)(Value % i), !IsMaxPlayer);
                }
            }
        }
    }
}
