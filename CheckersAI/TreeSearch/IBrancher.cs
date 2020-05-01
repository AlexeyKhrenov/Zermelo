namespace CheckersAI.TreeSearch
{
    internal interface IBrancher<TNode, TState, TMetric>
        where TNode : INode<TNode, TMetric>
        where TState : struct
        where TMetric : struct
    {
        void Branch(TNode node, TState state);
    }
}
