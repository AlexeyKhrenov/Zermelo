using Checkers;
using Checkers.Minifications;
using CheckersAI.InternalInterfaces;
using CheckersAI.TreeSearch;
using System;

namespace CheckersAI.CheckersGameTree
{
    internal class StateTransitions : IStateTransitions<BoardMinified, GameNode, sbyte>
    {
        private CheckersRules _rules;

        public StateTransitions(CheckersRules rules)
        {
            _rules = rules;
        }

        public BoardMinified GoDown(BoardMinified state, GameNode node)
        {
            // todo - take care about moves that need available moves to be known
            // todo - implement fast-forwarding move
            return _rules.FastForwardMove(state, node.Move);
        }

        public BoardMinified GoUp(BoardMinified state, GameNode node)
        {
            // todo - implement fast-forwarding
            return _rules.FastForwardUndoMove(state, node.Move, node.Parent?.Move);
        }

        public BoardMinified Copy(BoardMinified state)
        {
            // todo - change state.Pieces to fixed too
            var target = state;
            target.Pieces = state.Pieces.Clone() as BoardCell[,];
            return target;
        }
    }
}
