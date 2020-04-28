using System;
using System.Threading.Tasks;

using CheckersAI.TreeSearch;

namespace CheckersAI.ByteTree
{
    internal class BrancherMock : IBrancher<ByteNode, sbyte>
    {
        public void Branch(ByteNode node)
        {
            if (node.Children != null && node.Children.Length > 0)
            {
                throw new InvalidOperationException("Leave nodes are already created");
            }
        }
    }
}
