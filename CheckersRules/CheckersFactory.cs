using Game.PublicInterfaces;

namespace Checkers
{
    public class CheckersFactory : IGameFactory
    {
        public IGame CreateGame(int size, bool revertedSides)
        {
            var game = new Game.Game(new CheckersRules(size), size, revertedSides);
            return game;
        }
    }
}
