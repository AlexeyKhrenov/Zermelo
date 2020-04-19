using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Checkers.Minifications;
using Checkers.Rules;
using Game.PublicInterfaces;

namespace Checkers
{
    // todo - checkers rules appender or host or assembler
    internal class CheckersRules : IGameRules
    {
        private int _size;

        //todo - create several chains of responsibility for different situations
        private AbstractRule ChainOfRules;
        private AbstractRule InitialPositionRules;

        public CheckersRules(int Size)
        {
            _size = Size;
            ChainOfRules = new AppendMoveRule();
            ChainOfRules.AddNext(new RemoveCapturedPieceRule());
            ChainOfRules.AddNext(new ChangePieceTypeRule());
            ChainOfRules.AddNext(new NeedToContinueCaptureRule());
            ChainOfRules.AddNext(new SwitchPlayerRule());
            ChainOfRules.AddNext(new NeedToCaptureRule());
            ChainOfRules.AddNext(new DetectAvailableMovesRule());

            InitialPositionRules = new InitialPositionRule();
            InitialPositionRules.AddNext(new DetectAvailableMovesRule());
        }

        public IBoard CreateBoard(IPlayer player1, IPlayer player2, bool invertedCoordinates)
        {
            var board = new BoardMinified();

            InitialPositionRules.ApplyRule(board, null);

            return new Board().Restore(board, player1, player2);
        }

        public void MakeMove(IBoard board, IHistoryItem move)
        {
            ChainOfRules.ApplyRule(CheckInputBoard(board), move);
        }

        public void Undo(IBoard board, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            ChainOfRules.UndoRule(CheckInputBoard(board), toUndo, lastMoveBeforeUndo);
        }

        private BoardMinified CheckInputBoard(IBoard board)
        {
            if (board is BoardMinified)
            {
                return (BoardMinified) board;
            }
            else
            {
                throw new InvalidOperationException("The board was not created using checkers rules");
            }
        }
    }
}
