using CheckersAI.Tree;

namespace CheckersAI.MultithreadedTreeSearch
{
    internal abstract class AlfaBetaNodeBase<TNode, TValue, TMetric> : INode<TNode, TValue>
        where TNode : AlfaBetaNodeBase<TNode, TValue, TMetric> 
        where TValue : struct
        where TMetric : struct
    {
        public TMetric Alfa { get; set; }

        public TMetric Beta { get; set; }

        public bool IsAnnounced { get; set; }

        public bool IsCutOff { get; set; }

        public TNode Parent { get; set; }

        public bool IsFinalized { get; }

        public int Depth { get; set; }

        public TMetric Result { get; set; }

        public TValue Value { get; set; }

        public TNode[] Children { get; set; }

        public bool IsMaxPlayer { get; set; }

        public abstract void Update(TNode node);

        public abstract void UpdateFinalizedFlag(TNode node);

        public abstract void UpdateAlfaBeta(TMetric alfa, TMetric beta);

        public abstract void Clear();

        public abstract void UpdateTerminalNode(TMetric result);

        public abstract TNode CheckIfAnyParentNodesCuttedOff();
    }
}
