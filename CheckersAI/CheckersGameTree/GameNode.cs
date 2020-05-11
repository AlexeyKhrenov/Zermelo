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

        public bool IsEvaluated { get; set; }

        public bool WasCutoff { get; set; }

        public sbyte Result { get; set; }

        public GameNode Parent { get; set; }

        public GameNode[] Children { get; set; }

        public GameNode BestChild { get; set; }
        
        public HistoryItemMinified Move { get; set; }

        public bool IsMaxPlayer { get; set; }

        public sbyte Alfa { get; set; }

        public sbyte Beta { get; set; }

        public bool IsFinalized { get; set; }

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

        public GameNode(IHistoryItem item, IBoard board)
        {
            if (item != null)
            {
                Move = new HistoryItemMinified();
                Move.Minify(item, board);
                IsMaxPlayer = Move.Player;
            }
            else
            {
                IsMaxPlayer = true;
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

                var numberOfCutoffChildren = 0;
                foreach (var c in Children)
                {
                    if (!c.IsFinalized)
                    {
                        return;
                    }
                    if (c.WasCutoff)
                    {
                        numberOfCutoffChildren++;
                    }
                }

                if (numberOfCutoffChildren == Children.Length)
                {
                    WasCutoff = true;
                }

                IsFinalized = true;
            }
        }
    }
}
