using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CheckersAI.InternalInterfaces
{
    internal interface IProgressiveDeepeningWrapper<TNode, TMetric, TState>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        /// <summary>
        /// Returns chain of maximizing or minimizing nodes
        /// </summary>
        Queue<TNode> Run(TState state,
            TNode node,
            TMetric alfa,
            TMetric beta,
            int maxDepth,
            CancellationToken ct);
    }
}
