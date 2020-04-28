namespace CheckersAI.TreeSearch
{
    // the implementation may have different output range and therefore different return type
    internal interface IEvaluator<TNode, TMetric>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
    {
        TMetric Evaluate(TNode node);
    }
}
