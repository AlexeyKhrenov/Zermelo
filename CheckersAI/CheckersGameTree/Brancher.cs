using Checkers;
using Game.Primitives;
using System.Threading.Tasks;

namespace CheckersAI.CheckersGameTree
{
    internal class Brancher : IBrancher<GameNode, sbyte>
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
