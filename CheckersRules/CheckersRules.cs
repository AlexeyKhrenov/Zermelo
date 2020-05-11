using System.Runtime.CompilerServices;
using Checkers.Minifications;
using Checkers.Rules;
using Game.PublicInterfaces;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
[assembly: InternalsVisibleTo("CheckersAI")]
[assembly: InternalsVisibleTo("Benchmarking")]
namespace Checkers
{
    internal class CheckersRules : IGameRules
    {
        private AbstractRule ChainOfRules;
        private AbstractRule InitialPositionRules;

        // optimisation for StateTransitions
        private AbstractRule FastForwardAvailableMovesRules;
        private AbstractRule FastForwardMoveRules;
        private AbstractRule FastForwardUndoMoveRules;

        public CheckersRules()
        {
            ChainOfRules = new AppendMoveRule();
            ChainOfRules.AddNext(new RemoveCapturedPieceRule());
            ChainOfRules.AddNext(new ChangePieceTypeRule());
            ChainOfRules.AddNext(new NeedToContinueCaptureRule());
            ChainOfRules.AddNext(new SwitchPlayerRule());
            ChainOfRules.AddNext(new NeedToCaptureRule());
            ChainOfRules.AddNext(new DetectAvailableMovesRule());

            FastForwardMoveRules = new AppendMoveRule();
            FastForwardMoveRules.AddNext(new RemoveCapturedPieceRule());
            FastForwardMoveRules.AddNext(new NeedToContinueCaptureRule());
            FastForwardMoveRules.AddNext(new SwitchPlayerRule());

            FastForwardUndoMoveRules = new AppendMoveRule();
            FastForwardUndoMoveRules.AddNext(new RemoveCapturedPieceRule());
            FastForwardUndoMoveRules.AddNext(new NeedToContinueCaptureRule());
            FastForwardUndoMoveRules.AddNext(new SwitchPlayerRule());

            InitialPositionRules = new InitialPositionRule();
            InitialPositionRules.AddNext(new DetectAvailableMovesRule());

            FastForwardAvailableMovesRules = new NeedToCaptureRule();
            FastForwardAvailableMovesRules.AddNext(new DetectAvailableMovesRule());
        }

        public void PlaceFigures(IBoard board)
        {
            var min = board.ToMinified();
            min = InitialPositionRules.ApplyRule(min, null);   
            min.ToMaximized(board);
        }

        public IPlayer MakeMove(IBoard board, IHistoryItem move)
        {
            var minBoard = board.ToMinified();

            var minMove = new HistoryItemMinified();
            minMove.Minify(move, board);

            minBoard = MakeMove(minBoard, minMove);
            minBoard.ToMaximized(board);
            minMove.Maximize(move);

            if (minBoard.Player1PiecesCount == 0)
            {
                return board.Player1;
            }

            if (minBoard.Player2PiecesCount == 0)
            {
                return board.Player2;
            }
            return null;
        }

        public void Undo(IBoard board, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            var minBoard = board.ToMinified();

            var minToUndo = new HistoryItemMinified();
            minToUndo.Minify(toUndo, board);

            HistoryItemMinified minLastMove = null;
            if (lastMoveBeforeUndo != null)
            {
                minLastMove = new HistoryItemMinified();
                minLastMove.Minify(lastMoveBeforeUndo, board);
            }

            minBoard = ChainOfRules.UndoRule(minBoard, minToUndo, minLastMove);
            minBoard.ToMaximized(board);
        }

        internal BoardMinified MakeMove(BoardMinified board, HistoryItemMinified move)
        {
            return ChainOfRules.ApplyRule(board, move);
        }

        internal BoardMinified UndoMove(BoardMinified board, HistoryItemMinified move, HistoryItemMinified undoMove)
        {
            return ChainOfRules.UndoRule(board, move, undoMove);
        }

        internal BoardMinified FastForwardAvailableMoves(BoardMinified board)
        {
            return FastForwardAvailableMovesRules.ApplyRule(board, null);
        }

        internal BoardMinified FastForwardMove(BoardMinified board, HistoryItemMinified move)
        {
            return FastForwardMoveRules.ApplyRule(board, move);
        }

        internal BoardMinified FastForwardUndoMove(BoardMinified board, HistoryItemMinified move, HistoryItemMinified undoMove)
        {
            return FastForwardUndoMoveRules.UndoRule(board, move, undoMove);
        }
    }
}
