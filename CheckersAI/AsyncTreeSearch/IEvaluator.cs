using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CheckersAI.AsyncTreeSearch
{
    internal interface IEvaluator<TNode, TValue, TMetric>
        where TNode : IAlfaBetaNode<TNode, TValue, TMetric>
        where TValue : struct
        where TMetric : struct
    {
        Task<TMetric> Evaluate(TNode node);
    }
}
