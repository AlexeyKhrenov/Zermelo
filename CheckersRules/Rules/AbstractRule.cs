using Checkers.Minifications;
using Game.PublicInterfaces;

namespace Checkers.Rules
{
    internal abstract class AbstractRule
    {
        public abstract void ApplyRule(BoardMinified board, HistoryItemMinified move);

        public abstract void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo);

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

        protected void Next(BoardMinified board, HistoryItemMinified latestMove)
        {
            NextRule?.ApplyRule(board, latestMove);
        }

        protected void NextUndo(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            NextRule?.UndoRule(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
