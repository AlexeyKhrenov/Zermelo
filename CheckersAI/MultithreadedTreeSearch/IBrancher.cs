using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CheckersAI.MultithreadedTreeSearch
{
    internal interface IBrancher<TNode, TValue, TMetric>
        where TNode : AlfaBetaNodeBase<TNode, TValue, TMetric>
        where TValue : struct
        where TMetric : struct
    {
        Task Branch(TNode node);
    }
}
