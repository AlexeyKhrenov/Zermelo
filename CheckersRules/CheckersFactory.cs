using Game.Implementations;
using Game.PublicInterfaces;

namespace Checkers
{
    public class CheckersFactory : IGameFactory
    {
        public IGame CreateGame(int size, bool revertedSides)
        {
            var history = new History();
            var game = new Game.Implementations.Game(new CheckersRules(size), history, size, revertedSides);
            return game;
        }
    }
}
