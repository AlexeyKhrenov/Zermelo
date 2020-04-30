using Checkers.Minifications;
using Game.PublicInterfaces;

namespace Checkers.Rules
{
    internal abstract class AbstractRule
    {
        public abstract BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified move);

        public abstract BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo);

        private AbstractRule NextRule;

        public void AddNext(AbstractRule next)
        {
            if (NextRule != null)
            {
                NextRule.AddNext(next);
            }
            else
            {
                NextRule = next;
            }
        }

        protected BoardMinified Next(BoardMinified board, HistoryItemMinified latestMove)
        {
            if (NextRule != null)
            {
                return NextRule.ApplyRule(board, latestMove);
            }
            else
            {
                return board;
            }
        }

        protected BoardMinified NextUndo(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (NextRule != null)
            {
                return NextRule.UndoRule(board, toUndo, lastMoveBeforeUndo);
            }
            else
            {
                return board;
            }
        }
    }
}
