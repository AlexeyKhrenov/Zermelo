using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.Tree
{
    // such generic arguments to avoid unboxing
    internal interface INode<TNode, TValue> where TNode : INode<TNode, TValue> where TValue : struct
    {
        TValue Value { get; set; }

        TNode[] Children { get; set; }

        bool IsMaxPlayer { get; }
    }
}
