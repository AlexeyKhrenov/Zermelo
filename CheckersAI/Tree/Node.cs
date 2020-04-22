using Checkers.Minifications;
using Checkers.Primitives;
using CheckersAI.Evaluators;
using CheckersAI.TreeSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.Tree
{
    internal class Node<TValue>
    {
        public TValue Value;

        public Node<TValue>[] Children;

        public bool IsMaxPlayer;

        public bool IsVisited;

        public Node(TValue value, bool isMaxPlayer, params Node<TValue>[] children)
        {
            IsMaxPlayer = isMaxPlayer;
            Children = children;
            Value = value;
        }
    }
}
