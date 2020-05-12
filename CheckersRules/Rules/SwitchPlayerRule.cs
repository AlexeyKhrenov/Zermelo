using Checkers.Minifications;

namespace Checkers.Rules
{
    internal class SwitchPlayerRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board.SwitchPlayers();
            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo,
            HistoryItemMinified lastMoveBeforeUndo)
        {
            if (board.ActivePlayer != toUndo.Player) board.SwitchPlayers();

            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}