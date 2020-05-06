using System.Runtime.CompilerServices;
using Checkers.Minifications;
using Checkers.Rules;
using Game.PublicInterfaces;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
[assembly: InternalsVisibleTo("CheckersAI")]
[assembly: InternalsVisibleTo("Benchmarking")]
namespace Checkers
{
    // todo - checkers rules appender or host or assembler
    internal class CheckersRules : IGameRules
    {
        //todo - create several chains of responsibility for different situations
        private AbstractRule ChainOfRules;
        private AbstractRule InitialPositionRules;
        private AbstractRule FastForwardAvailableMovesRules;

        public CheckersRules()
        {
            ChainOfRules = new AppendMoveRule();
            ChainOfRules.AddNext(new RemoveCapturedPieceRule());
            ChainOfRules.AddNext(new ChangePieceTypeRule());
            ChainOfRules.AddNext(new NeedToContinueCaptureRule());
            ChainOfRules.AddNext(new SwitchPlayerRule());
            ChainOfRules.AddNext(new NeedToCaptureRule());
            ChainOfRules.AddNext(new DetectAvailableMovesRule());

            InitialPositionRules = new InitialPositionRule();
            InitialPositionRules.AddNext(new DetectAvailableMovesRule());

            FastForwardAvailableMovesRules = new NeedToCaptureRule();
            FastForwardAvailableMovesRules.AddNext(new DetectAvailableMovesRule());
        }

        public void PlaceFigures(IBoard board)
        {
            var min = board.ToMinified();
            min = InitialPositionRules.ApplyRule(min, null);   
            min.Maximize(board);
        }

        public void MakeMove(IBoard board, IHistoryItem move)
        {
            var minBoard = board.ToMinified();

            var minMove = new HistoryItemMinified();
            minMove.Minify(move, board);

            minBoard = MakeMove(minBoard, minMove);
            minBoard.Maximize(board);
            minMove.Maximize(move);
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
            minBoard.Maximize(board);
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
    }
}
