using Game.Implementations;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public static class ServiceLocator
    {
        public static IGame CreateGame
        (
            IGameRules rules,
            IPlayer player1,
            IPlayer player2,
            int gameSize
        )
        {
            var history = new History();
            return new Implementations.Game(rules, player1, player2, history, gameSize);
        }
    }
}
