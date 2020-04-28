using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    // such generic arguments to avoid unboxing
    internal interface INode<TNode, TMetric> where TMetric : struct where TNode : INode<TNode, TMetric>
    {
        TNode[] Children { get; set; }

        bool IsMaxPlayer { get; }
    }
}
