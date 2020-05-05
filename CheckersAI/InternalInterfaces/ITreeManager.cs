namespace CheckersAI.InternalInterfaces
{
    internal interface ITreeManager<TNode, TMetric>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
    {
        TNode GoDownToNode(TNode node);
    }
}
