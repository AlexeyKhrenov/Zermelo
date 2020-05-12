namespace CheckersAI.InternalInterfaces
{
    internal interface IAlfaBetaNode<TNode, TMetric> : INode<TNode, TMetric> 
        where TNode : class, INode<TNode, TMetric>
        where TMetric : struct
    {
        TMetric Alfa { get; set; }

        TMetric Beta { get; set; }

        TNode Parent { get; }

        bool TryLockNode(); //todo - rename

        bool IsFinalized { get; }

        void Update(TNode child);

        void Update(TMetric result);

        void UpdateAlfaBeta(TNode parent);

        bool WasCutoff { get; set; }

        TMetric TerminationResult { get; set; }

        bool IsEvaluated { get; set; }
    }
}
