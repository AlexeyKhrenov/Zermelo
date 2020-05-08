namespace CheckersAI.InternalInterfaces
{
    internal interface IStateTransitions<TState, TNode, TMetric>
        where TNode : INode<TNode, TMetric>
        where TState : struct
        where TMetric : struct
    {
        TState GoUp(TState state, TNode node);

        TState GoDown(TState state, TNode node);

        TState Copy(TState state);
    }
}
