using CheckersAI.Tree;
using CheckersAI.TreeSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking.ByteTree
{
    internal class BrancherMock : IBrancher<ByteNode, byte>
    {
        public void Branch(INode<ByteNode, byte> node)
        {
            if (node.Children != null && node.Children.Length > 0)
            {
                throw new InvalidOperationException("Leave nodes are already created");
            }
        }
    }
}
