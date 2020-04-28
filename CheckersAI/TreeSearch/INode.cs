using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    // such generic arguments to avoid unboxing
    internal interface INode<TNode> where TNode : INode<TNode>
    {
        TNode[] Children { get; set; }

        bool IsMaxPlayer { get; }
    }
}
