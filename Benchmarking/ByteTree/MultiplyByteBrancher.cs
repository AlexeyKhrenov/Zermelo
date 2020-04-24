using CheckersAI.Tree;
using CheckersAI.TreeSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking.ByteTree
{
    internal class MultiplyByteBrancher : IBrancher<ByteNode, byte>
    {
        private byte _branchingFactor;

        public MultiplyByteBrancher(byte branchingFactor)
        {
            _branchingFactor = branchingFactor;
        }

        public void Branch(ByteNode node)
        {
            if (node.Children != null && node.Children.Length > 0)
            {
                throw new InvalidOperationException("Leave nodes are already created");
            }
            else
            {
                node.Children = new ByteNode[_branchingFactor];
                for (var i = 0; i < node.Children.Length; i++)
                {
                    node.Children[i] = new ByteNode((byte)(node.Value * (i + 1)), !node.IsMaxPlayer);
                }
            }
        }
    }
}
