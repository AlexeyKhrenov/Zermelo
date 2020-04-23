using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.Tree
{
    internal interface IAlfaBetaNode<TNode, TValue, TMetric> : INode<TNode, TValue> where TNode : INode<TNode, TValue> where TValue : struct
    {
        TMetric Alfa { get; set; }

        TMetric Beta { get; set; }

        TNode Parent { get; set; }
    }
}
