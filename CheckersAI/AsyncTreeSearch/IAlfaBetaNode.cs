using CheckersAI.Tree;

namespace CheckersAI.AsyncTreeSearch
{
    internal interface IAlfaBetaNode<TNode, TValue, TMetric> : INode<TNode, TValue>
        where TNode : IAlfaBetaNode<TNode, TValue, TMetric> 
        where TValue : struct
        where TMetric : struct
    {
        TMetric Alfa { get; set; }

        TMetric Beta { get; set; }

        TNode Parent { get; set; }
    }
}
