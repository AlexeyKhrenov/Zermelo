namespace CheckersAI.InternalInterfaces
{
    internal interface IAlfaBetaNode<TNode, TMetric> : INode<TNode, TMetric> 
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
    {
        TMetric Alfa { get; set; }

        TMetric Beta { get; set; }

        TMetric Result { get; set; }
    }
}
