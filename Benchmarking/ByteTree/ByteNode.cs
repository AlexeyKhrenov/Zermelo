﻿using CheckersAI.Tree;
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
    }
}