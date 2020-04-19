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

        public void PlaceFigures(IBoard board)
        {
            var min = new BoardMinified();
            InitialPositionRules.ApplyRule(min, null);   
            min.Maximize(board);
        }

        public void MakeMove(IBoard board, IHistoryItem move)
        {
            var minBoard = new BoardMinified();
            minBoard.Minify(board);

            var minMove = new HistoryItemMinified();
            minMove.Minify(move);

            ChainOfRules.ApplyRule(minBoard, minMove);
            minBoard.Maximize(board);
        }

        public void Undo(IBoard board, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            var minBoard = new BoardMinified();
            minBoard.Minify(board);

            var minToUndo = new HistoryItemMinified();
            minToUndo.Minify(toUndo);

            var minLastMove = new HistoryItemMinified();
            minLastMove.Minify(toUndo);

            ChainOfRules.UndoRule(minBoard, minToUndo, minLastMove);
        }
    }
}
