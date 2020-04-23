using Checkers.Minifications;
using CheckersAI.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    // the implementation may have different output range and therefore different return type
    internal interface IEvaluator<TNode, TValue, TMetric>
        where TNode : INode<TNode, TValue>
        where TValue : struct
        where TMetric : struct
    {
        TMetric Evaluate(TNode node);
    }
}
