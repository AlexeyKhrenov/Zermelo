using Checkers;
using CheckersAI.TreeSearch;

namespace CheckersAI.CheckersGameTree
{
    internal class Brancher : IBrancher<GameNode>
    {
        private CheckersRules _rules;

        public Brancher(CheckersRules rules)
        {
            _rules = rules;
        }

        public void Branch(GameNode node)
        {
        }
    }
}
