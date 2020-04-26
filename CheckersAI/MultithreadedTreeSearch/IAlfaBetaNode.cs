using CheckersAI.Tree;

namespace CheckersAI.MultithreadedTreeSearch
{
    internal interface IAlfaBetaNode<TNode, TValue, TMetric> : INode<TNode, TValue>
        where TNode : IAlfaBetaNode<TNode, TValue, TMetric> 
        where TValue : struct
        where TMetric : struct
    {
        TMetric Alfa { get; set; }

        TMetric Beta { get; set; }

        bool IsAnnounced { get; set; }

        bool IsCutOff { get; set; }

        TNode Parent { get; set; }

        bool IsFinalized { get; }
        
        int Depth { get; set; }

        TMetric Result { get; set; }

        void Update(TNode node);

        void UpdateFinalizedFlag(TNode node);

        void UpdateAlfaBeta(TMetric alfa, TMetric beta);

        void Clear();

        void UpdateTerminalNode(TMetric result);

        TNode CheckIfAnyParentNodesCuttedOff();
    }
}
