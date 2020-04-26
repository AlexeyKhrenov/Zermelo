using Checkers.Minifications;
using CheckersAI.ByteTree;
using CheckersAI.MultithreadedTreeSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.CheckersGameTree
{
    internal class GameNode : AlfaBetaByteNode
    {
        public GameNode(sbyte value, bool isMaxPlayer, params AlfaBetaByteNode[] children) : base(value, isMaxPlayer, children)
        {
        }
    }
}
