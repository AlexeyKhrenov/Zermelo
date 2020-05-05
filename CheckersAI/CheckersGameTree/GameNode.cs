using Checkers.Minifications;
using CheckersAI.InternalInterfaces;
using Game.Primitives;
using Game.PublicInterfaces;
using System;

namespace CheckersAI.CheckersGameTree
{
    internal class GameNode : INode<GameNode, sbyte>
    {
        public sbyte Result { get; set; }

        public GameNode Parent { get; set; }

        public GameNode[] Children { get; set; }

        public GameNode BestChild { get; set; }
        
        public HistoryItemMinified Move { get; set; }

        public bool IsMaxPlayer { get; set; }

        public GameNode()
        {
            IsMaxPlayer = true;
            Result = sbyte.MinValue;
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
            BestChild = null;
            Result = sbyte.MinValue;
        }

        public override string ToString()
        {
            return $"{Move.From.X},{Move.From.Y}->{Move.To.X},{Move.To.Y}  Result:{Result}";
        }
    }
}
