using CheckersAI.Tree;

namespace CheckersAI.MultithreadedTreeSearch
{
    internal abstract class AlfaBetaNodeBase<TNode, TValue, TMetric> : INode<TNode, TValue>
        where TNode : AlfaBetaNodeBase<TNode, TValue, TMetric> 
        where TValue : struct
        where TMetric : struct
    {
        protected uint _childAddressBit;
        protected uint _finalizedFlag;

        public bool IsFinalized => _finalizedFlag == 0;

        public TMetric Alfa { get; set; }

        public TMetric Beta { get; set; }

        public bool IsAnnounced { get; set; }

        public bool IsCutOff { get; set; }

        public TNode Parent { get; set; }

        public int Depth { get; set; }

        public TMetric Result { get; set; }

        public TValue Value { get; set; }

        public TNode[] Children { get; set; }

        public bool IsMaxPlayer { get; protected set; }

        public abstract void Update(TNode node);

        public abstract void UpdateAlfaBeta(TMetric alfa, TMetric beta);

        public abstract void Clear();

        public TNode CheckIfAnyParentNodesCuttedOff()
        {
            var parent = Parent;
            while (parent != null)
            {
                if (parent.IsCutOff)
                {
                    return parent;
                }
                parent = parent.Parent;
            }

            return null;
        }

        private object _obj2 = new object();
        public void UpdateFinalizedFlag(TNode node)
        {
            var finalizedBit = node._childAddressBit;

            lock (_obj2)
            {
                _finalizedFlag &= ~finalizedBit;
            }
        }

        public void UpdateTerminalNode(TMetric result)
        {
            Result = result;
            _finalizedFlag = 0;
        }
    }
}
