using System.Runtime.CompilerServices;
using Checkers.Minifications;

namespace Checkers.Rules
{
    internal abstract class AbstractRule
    {
        private AbstractRule NextRule;
        public abstract BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified move);

        public abstract BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo,
            HistoryItemMinified lastMoveBeforeUndo);

        public void AddNext(AbstractRule next)
        {
            if (NextRule != null)
                NextRule.AddNext(next);
            else
                NextRule = next;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected BoardMinified Next(BoardMinified board, HistoryItemMinified latestMove)
        {
            if (NextRule != null)
                return NextRule.ApplyRule(board, latestMove);
            return board;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected BoardMinified NextUndo(BoardMinified board, HistoryItemMinified toUndo,
            HistoryItemMinified lastMoveBeforeUndo)
        {
            if (NextRule != null)
                return NextRule.UndoRule(board, toUndo, lastMoveBeforeUndo);
            return board;
        }
    }
}