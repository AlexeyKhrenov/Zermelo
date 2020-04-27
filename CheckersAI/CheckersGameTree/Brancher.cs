
using Checkers;
using Checkers.Primitives;
using CheckersAI.MultithreadedTreeSearch;
using Game.PublicInterfaces;
using System.Threading.Tasks;

namespace CheckersAI.CheckersGameTree
{
    internal class Brancher : IBrancher<GameNode, Move, sbyte>
    {
        private CheckersRules _rules;

        public Brancher(CheckersRules rules)
        {
            _rules = rules;
        }
        public void Branch(GameNode node)
        {
        }

        Task IBrancher<GameNode, Move, sbyte>.Branch(GameNode node)
        {
            throw new System.NotImplementedException();
        }
    }
}
