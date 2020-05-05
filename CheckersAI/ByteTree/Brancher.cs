﻿using System;
using CheckersAI.InternalInterfaces;

namespace CheckersAI.ByteTree
{
    internal class Brancher : IBrancher<ByteNode, sbyte, sbyte>
    {
        public void Branch(ByteNode node, sbyte state)
        {
            if (node.Children != null && node.Children.Length > 0)
            {
                throw new InvalidOperationException("Leave nodes are already created");
            }
        }
    }
}
