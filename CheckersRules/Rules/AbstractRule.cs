using Game.PublicInterfaces;

namespace Checkers.Rules
{
    internal abstract class AbstractRule
    {
        public abstract string Name { get; }

        public abstract void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove);

        public abstract void UndoRule(IGame game, Piece[,] pieces, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo);

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

        protected void Next(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            NextRule?.ApplyRule(game, pieces, latestMove);
        }

        protected void NextUndo(IGame game, Piece[,] pieces, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            NextRule?.UndoRule(game, pieces, toUndo, lastMoveBeforeUndo);
        }
    }
}
