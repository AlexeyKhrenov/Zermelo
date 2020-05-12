using Checkers.Minifications;
using CheckersAI.InternalInterfaces;
using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CheckersAI.CheckersGameTree
{
    internal class GameNode : INode<GameNode, sbyte>, IAlfaBetaNode<GameNode, sbyte>
    {
        public sbyte TerminationResult { get; set; }

        public sbyte Result { get; set; }

        public GameNode Parent { get; set; }

        public GameNode[] Children { get; set; }

        public GameNode BestChild { get; set; }

        public HistoryItemMinified Move { get; set; }

        private volatile sbyte _alfa;
        public sbyte Alfa { get { return _alfa; } set { _alfa = value; } }

        private volatile sbyte _beta;
        public sbyte Beta { get { return _beta; } set { _beta = value; } }

        public bool IsFinalized
        {
            get { return (_state & (byte)GameNodeType.IsFinalized) != 0; }
            set
            {
                if (value == true)
                {
                    _state = (byte)(_state | (byte)GameNodeType.IsFinalized);
                }
                else
                {
                    _state = (byte)(_state & ~(byte)GameNodeType.IsFinalized);
                }
            }
        }

        public bool IsEvaluated
        {
            get { return (_state & (byte)GameNodeType.IsEvaluated) != 0; }
            set
            {
                if (value == true)
                {
                    _state = (byte)(_state | (byte)GameNodeType.IsEvaluated);
                }
                else
                {
                    _state = (byte)(_state & ~(byte)GameNodeType.IsEvaluated);
                }
            }
        }

        public bool WasCutoff
        {
            get { return (_state & (byte)GameNodeType.WasCutOff) != 0; }
            set
            {
                if (value == true)
                {
                    _state = (byte)(_state | (byte)GameNodeType.WasCutOff);
                }
                else
                {
                    _state = (byte)(_state & ~(byte)GameNodeType.WasCutOff);
                }
            }
        }

        public bool IsMaxPlayer
        {
            get { return (_state & (byte)GameNodeType.IsMaxPlayer) != 0; }
            set
            {
                if (value == true)
                {
                    _state = (byte)(_state | (byte)GameNodeType.IsMaxPlayer);
                }
                else
                {
                    _state = (byte)(_state & ~(byte)GameNodeType.IsMaxPlayer);
                }
            }
        }

        private byte _state;
        internal int _expectedFinalizedFlag;
        internal int _cutoffChildrenFlag;
        internal int _isFinalizedFlag;
        internal int _childAddressBit;

        public GameNode()
        {
            IsMaxPlayer = true;
            Clear();
        }

        public GameNode(bool isMaxPlayer)
        {
            IsMaxPlayer = isMaxPlayer;
            Clear();
        }

        public GameNode(IHistoryItem item, IBoard board, bool isMaxPlayer)
        {
            if (item != null)
            {
                Move = new HistoryItemMinified();
                Move.Minify(item, board);
                IsMaxPlayer = isMaxPlayer;
            }
            
            Clear();
        }

        // don't call this method often - not optimized
        public Move GetBestMove()
        {
            if (Children != null && Children.Length != 0)
            {
                foreach (var child in Children)
                {
                    if (child.Result == Result)
                    {
                        return new Move(child.Move.From, child.Move.To);
                    }
                }
            }

            throw new InvalidOperationException("A node is not evaluated yet");
        }

        // todo - implement
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(GameNode other)
        {
            return other != null && Move == other.Move;
        }

        public void Clear()
        {
            Alfa = sbyte.MinValue;
            Beta = sbyte.MaxValue;
            IsFinalized = false;
            _isLocked = 0;
            _isFinalizedFlag = 0;
            _cutoffChildrenFlag = 0;

            if (IsMaxPlayer)
            {
                Result = sbyte.MinValue;
            }
            else
            {
                Result = sbyte.MaxValue;
            }

            BestChild = null;
        }

        public override string ToString()
        {
            if (Move != null)
            {
                return $"{Move.From.X},{Move.From.Y}->{Move.To.X},{Move.To.Y}  Result:{Result}";
            }
            else
            {
                return $"Move is NULL. Result: {Result}";
            }
        }

        public void Update(sbyte result)
        {
            IsFinalized = true;
            Result = result;
        }

        private int _isLocked;
        public bool TryLockNode()
        {
            return 0 == Interlocked.CompareExchange(ref _isLocked, 1, 0);
        }

        public void UpdateAlfaBeta(GameNode parent)
        {
            Alfa = parent.Alfa;
            Beta = parent.Beta;
        }

        // todo - consider making it thread-safe
        object _lock1 = new object();
        public void Update(GameNode child)
        {
            lock (_lock1)
            {
                if (!child.WasCutoff)
                {
                    _isFinalizedFlag |= child._childAddressBit;

                    if (IsMaxPlayer)
                    {
                        if (child.Result >= Result)
                        {
                            Result = child.Result;
                            BestChild = child;
                        }
                        Alfa = Result > Alfa ? Result : Alfa;
                    }
                    else
                    {
                        if (child.Result <= Result)
                        {
                            Result = child.Result;
                            BestChild = child;
                        }
                        Beta = Result < Beta ? Result : Beta;
                    }

                    if (Alfa > Beta)
                    {
                        WasCutoff = true;
                        IsFinalized = true;
                    }
                }
                else
                {
                    _isFinalizedFlag |= child._childAddressBit;
                    _cutoffChildrenFlag |= child._childAddressBit;

                    if (_cutoffChildrenFlag == _expectedFinalizedFlag)
                    {
                        WasCutoff = true;
                        IsFinalized = true;
                        return;
                    }
                }
            }
            if (_isFinalizedFlag == _expectedFinalizedFlag)
            {
                IsFinalized = true;
            }
        }
    }
}
