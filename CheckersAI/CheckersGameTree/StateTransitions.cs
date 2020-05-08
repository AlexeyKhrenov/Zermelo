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
            return _rules.MakeMove(state, node.Move);
        }

        public BoardMinified GoUp(BoardMinified state, GameNode node)
        {
            // todo - implement fast-forwarding
            return _rules.UndoMove(state, node.Move, node.Parent?.Move);
        }

        public BoardMinified Copy(BoardMinified state)
        {
            // todo - improve perfomance
            var target = state;
            target.Pieces = state.Pieces.Clone() as BoardCell[,];
            target.Player1Pieces = new PieceMinified[state.Player1Pieces.Length];
            target.Player2Pieces = new PieceMinified[state.Player2Pieces.Length];
            state.Player1Pieces.CopyTo(target.Player1Pieces, 0);
            state.Player2Pieces.CopyTo(target.Player2Pieces, 0);
            return target;
        }
    }
}
