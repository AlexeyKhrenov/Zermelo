using CheckersAI.Tree;
using CheckersAI.TreeSearch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarking.ByteTree
{
    internal class BrancherMock :
        CheckersAI.TreeSearch.IBrancher<ByteNode, byte>,
        CheckersAI.AsyncTreeSearch.IBrancher<AlfaBetaByteNode, byte, byte>
    {
        public void Branch(ByteNode node)
        {
            if (node.Children != null && node.Children.Length > 0)
            {
                throw new InvalidOperationException("Leave nodes are already created");
            }
        }

        public async Task Branch(AlfaBetaByteNode node)
        {
            if (node.Children != null && node.Children.Length > 0)
            {
                throw new InvalidOperationException("Leave nodes are already created");
            }

            await Task.CompletedTask;
        }
    }
}
