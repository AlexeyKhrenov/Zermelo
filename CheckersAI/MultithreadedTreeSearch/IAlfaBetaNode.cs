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

        uint FinalizedFlag { get; set; }

        uint ChildAddressBit { get; set; }

        int Depth { get; set; }

        TMetric Result { get; set; }

        void Update(TMetric newValue, uint finalizedBit);

        void UpdateFinalizedFlag(uint address);

        void UpdateAlfaBeta(TMetric alfa, TMetric beta);
    }
}
