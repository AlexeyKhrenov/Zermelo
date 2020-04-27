using System;
using Checkers.Minifications;
using Checkers.Primitives;
using CheckersAI.MultithreadedTreeSearch;

namespace CheckersAI.CheckersGameTree
{
    internal class GameNode : AlfaBetaNodeBase<GameNode, Move, sbyte>
    {
        public BoardMinified Board { get; set; }

        public GameNode(bool isMaxPlayer)
        {
            IsMaxPlayer = isMaxPlayer;
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameNode node)
        {
            throw new NotImplementedException();
        }

        public override void UpdateAlfaBeta(sbyte alfa, sbyte beta)
        {
            throw new NotImplementedException();
        }
    }
}
