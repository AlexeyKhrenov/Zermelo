using System;
using System.Threading;
using CheckersAI.InternalInterfaces;

namespace CheckersAI.ByteTree
{
    public class AlfaBetaByteNode : IAlfaBetaNode<AlfaBetaByteNode, sbyte>
    {
        private int _isLocked;

        // todo - consider making it thread-safe
        private readonly object _lock1 = new object();

        private AlfaBetaByteNode(bool isMaxPlayer)
        {
            IsMaxPlayer = isMaxPlayer;
            Clear();
        }

        public AlfaBetaByteNode(sbyte valueChange, bool isMaxPlayer, AlfaBetaByteNode[] children) : this(isMaxPlayer)
        {
            ValueChange = valueChange;
            Children = children;

            foreach (var child in Children) child.Parent = this;
        }

        public sbyte ValueChange { get; }
        public sbyte TerminationResult { get; set; }

        public bool IsEvaluated { get; set; }

        public sbyte Alfa { get; set; }

        public sbyte Beta { get; set; }

        public AlfaBetaByteNode Parent { get; set; }

        public bool IsFinalized { get; set; }

        public AlfaBetaByteNode[] Children { get; set; }

        public bool IsMaxPlayer { get; }

        public sbyte Result { get; set; }

        public AlfaBetaByteNode BestChild { get; set; }

        public bool WasCutoff { get; set; }

        public void Clear()
        {
            Alfa = sbyte.MinValue;
            Beta = sbyte.MaxValue;
            IsFinalized = false;
            _isLocked = 0;

            if (IsMaxPlayer)
                Result = sbyte.MinValue;
            else
                Result = sbyte.MaxValue;
        }

        public bool Equals(AlfaBetaByteNode other)
        {
            throw new NotImplementedException();
        }

        public bool TryLockNode()
        {
            return 0 == Interlocked.CompareExchange(ref _isLocked, 1, 0);
        }

        public void UpdateAlfaBeta(AlfaBetaByteNode parent)
        {
            Alfa = parent.Alfa;
            Beta = parent.Beta;
        }

        public void Update(AlfaBetaByteNode child)
        {
            lock (_lock1)
            {
                if (!child.WasCutoff)
                {
                    if (IsMaxPlayer)
                    {
                        Result = child.Result > Result ? child.Result : Result;
                        Alfa = Result > Alfa ? Result : Alfa;
                    }
                    else
                    {
                        Result = child.Result < Result ? child.Result : Result;
                        Beta = Result < Beta ? Result : Beta;
                    }

                    if (Alfa > Beta)
                    {
                        WasCutoff = true;
                        IsFinalized = true;
                        return;
                    }
                }

                foreach (var c in Children)
                    if (!c.IsFinalized)
                        return;
                IsFinalized = true;
            }
        }

        public void Update(sbyte result)
        {
            IsFinalized = true;
            Result = result;
        }
    }
}