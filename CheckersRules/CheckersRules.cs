using System.Runtime.CompilerServices;
using Checkers.Minifications;
using Checkers.Rules;
using Game.PublicInterfaces;

[assembly: InternalsVisibleTo("ZermeloUnitTests")]
[assembly: InternalsVisibleTo("CheckersAI")]
namespace Checkers
{
    // todo - checkers rules appender or host or assembler
    internal class CheckersRules : IGameRules
    {
        //todo - create several chains of responsibility for different situations
        private AbstractRule ChainOfRules;
        private AbstractRule InitialPositionRules;

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
        }

        public void PlaceFigures(IBoard board)
        {
            var min = new BoardMinified();
            min.Minify(board);
            min = InitialPositionRules.ApplyRule(min, null);   
            min.Maximize(board);
        }

        public void MakeMove(IBoard board, IHistoryItem move)
        {
            var minBoard = new BoardMinified();
            minBoard.Minify(board);

            var minMove = new HistoryItemMinified();
            minMove.Minify(move, board);

            minBoard = MakeMove(minBoard, minMove);
            minBoard.Maximize(board);
            minMove.Maximize(move);
        }

        public void Undo(IBoard board, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            var minBoard = new BoardMinified();
            minBoard.Minify(board);

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
    }
}
