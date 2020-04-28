namespace CheckersAI.TreeSearch
{
    internal interface IBrancher<TNode, TMetric> where TNode : INode<TNode, TMetric> where TMetric : struct
    {
        void Branch(TNode node);
    }
}
