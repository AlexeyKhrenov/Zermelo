using System.Collections.Generic;
using System.Threading;

namespace CheckersAI.InternalInterfaces
{
    internal interface ISearch<TNode, TMetric, TState>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state, CancellationToken cancellationToken);

        void ClearTree(TNode node);

        /// <summary>
        /// Returns chain of maximizing nodes
        /// </summary>
        Queue<TNode> DoProgressiveDeepening(TNode node, TState state, TMetric alfa, TMetric beta, int maxDepth, CancellationToken ct);
    }
}
