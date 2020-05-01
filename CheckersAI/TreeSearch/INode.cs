using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    // such generic arguments to avoid unboxing
    internal interface INode<TNode, TMetric> where TNode : INode<TNode, TMetric> where TMetric : struct
    {
        TNode[] Children { get; set; }

        bool IsMaxPlayer { get; }

        TMetric Result { get; set; }
    }
}
