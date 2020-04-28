namespace CheckersAI.TreeSearch
{
    internal interface IAlfaBetaNode<TNode, TMetric> : INode<TNode> 
        where TNode : INode<TNode>
        where TMetric : struct
    {
        TMetric Alfa { get; set; }

        TMetric Beta { get; set; }

        TMetric Result { get; set; }
    }
}
