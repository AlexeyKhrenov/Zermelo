using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    public abstract class AbstractRule
    {
        public abstract void ApplyRule(IGame game);

        private AbstractRule NextRule;

        public void AddNextRuleInChain(AbstractRule next)
        {
            NextRule = next;
        }

        protected void PassControlToNext(IGame game)
        {
            NextRule?.ApplyRule(game);
        }
    }
}
