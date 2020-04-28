namespace CheckersAI.TreeSearch
{
    internal interface IBrancher<TNode> where TNode : INode<TNode>
    {
        void Branch(TNode node);
    }
}
