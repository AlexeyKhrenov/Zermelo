using Game.Implementations;
using Game.PublicInterfaces;

namespace Game
{
    public static class ServiceLocator
    {
        public static IGame CreateGame
        (
            IGameRules rules,
            IPlayer player1,
            IPlayer player2,
            byte gameSize
        )
        {
            var history = new History();
            return new Implementations.Game(rules, player1, player2, history, gameSize);
        }
    }
}