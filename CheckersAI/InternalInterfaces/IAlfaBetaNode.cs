namespace CheckersAI.InternalInterfaces
{
    internal interface IAlfaBetaNode<TNode, TMetric> : INode<TNode, TMetric>
        where TNode : class, INode<TNode, TMetric>
        where TMetric : struct
    {
        TNode Parent { get; }

        bool IsFinalized { get; }

        TMetric TerminationResult { get; }

        bool IsEvaluated { get; }

        bool TryLockNode();

        void Update(TNode child);

        void Update(TMetric result);

        void UpdateAlfaBeta(TNode parent);
    }
}