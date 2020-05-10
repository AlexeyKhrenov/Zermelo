using CheckersAI.InternalInterfaces;
using System;
using System.Threading;

namespace CheckersAI.ByteTree
{
    public class AlfaBetaByteNode : IAlfaBetaNode<AlfaBetaByteNode, sbyte>
    {
        public bool WasSplitted { get; set; }

        public sbyte ValueChange { get; set; }

        public sbyte Alfa { get; set; }

        public sbyte Beta { get; set; }

        public AlfaBetaByteNode Parent { get; set; }

        public bool IsFinalized { get; set; }

        public AlfaBetaByteNode[] Children { get; set; }

        public bool IsMaxPlayer { get; private set; }

        public sbyte Result { get; set; }

        public AlfaBetaByteNode BestChild { get; set; }

        public bool WasCutoff { get; set; }

        public AlfaBetaByteNode(bool isMaxPlayer)
        {
            IsMaxPlayer = isMaxPlayer;
            Clear();
        }

        public AlfaBetaByteNode(sbyte valueChange, bool isMaxPlayer, AlfaBetaByteNode[] children) : this(isMaxPlayer)
        {
            ValueChange = valueChange;
            Children = children;

            foreach (var child in Children)
            {
                child.Parent = this;
            }
        }

        public void Clear()
        {
            Alfa = sbyte.MinValue;
            Beta = sbyte.MaxValue;
            IsFinalized = false;
            _isLocked = 0;

            if (IsMaxPlayer)
            {
                Result = sbyte.MinValue;
            }
            else
            {
                Result = sbyte.MaxValue;
            }
        }

        public bool Equals(AlfaBetaByteNode other)
        {
            throw new NotImplementedException();
        }

        private int _isLocked;
        public bool TryLockNode()
        {
            return 0 == Interlocked.CompareExchange(ref _isLocked, 1, 0);
        }

        // todo - consider making it thread-safe
        object _lock1 = new object();
        public void Update(AlfaBetaByteNode child)
        {
            lock (_lock1)
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

                foreach (var c in Children)
                {
                    if (!c.IsFinalized)
                    {
                        return;
                    }
                }
            }

            IsFinalized = true;
        }

        public void Update(sbyte result)
        {
            IsFinalized = true;
            Result = result;
        }
    }
}
