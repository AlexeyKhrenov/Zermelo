using System.Threading;

namespace CheckersAI.InternalInterfaces
{
    internal interface ISearch<TNode, TMetric, TState>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state,
            CancellationToken cancellationToken);

        int EstimateRequiredMemoryUsageIncrementInMb(int startDepth, int endDepth);
    }
}