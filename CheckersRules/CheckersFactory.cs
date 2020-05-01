using Game.Implementations;
using Game.PublicInterfaces;

namespace Checkers
{
    public class CheckersFactory : IGameFactory
    {
        public IGame CreateGame(int size, bool revertedSides, IPlayer player1, IPlayer player2)
        {
            var history = new History();
            var game = new Game.Implementations.Game(new CheckersRules(), player1, player2, history, size, revertedSides);
            return game;
        }
    }
}
