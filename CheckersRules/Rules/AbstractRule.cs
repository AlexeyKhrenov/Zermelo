using Game.PublicInterfaces;

namespace Checkers.Rules
{
    internal abstract class AbstractRule
    {
        public abstract string Name { get; }

        public abstract void ApplyRule(IGame game, Piece[,] pieces);

        private AbstractRule NextRule;

        public void AddNextRuleInChain(AbstractRule next)
        {
            NextRule = next;
        }

        protected void PassControlToNext(IGame game, Piece[,] pieces)
        {
            NextRule?.ApplyRule(game, pieces);
        }
    }
}
