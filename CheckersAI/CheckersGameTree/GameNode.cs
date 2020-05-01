using Checkers.Minifications;
using CheckersAI.TreeSearch;
using Game.Primitives;
using System;

namespace CheckersAI.CheckersGameTree
{
    internal class GameNode : INode<GameNode, sbyte>
    {
        public sbyte Result { get; set; }

        public GameNode Parent { get; set; }

        public GameNode[] Children { get; set; }

        public HistoryItemMinified Move { get; set; }

        public bool IsMaxPlayer { get; set; }

        public GameNode()
        {
            Result = sbyte.MinValue;
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

        public override string ToString()
        {
            return $"{Move.From.X},{Move.From.Y}->{Move.To.X},{Move.To.Y}  Result:{Result}";
        }
    }
}
