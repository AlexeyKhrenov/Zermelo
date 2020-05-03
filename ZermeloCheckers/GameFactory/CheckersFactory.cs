using CheckersAI;
using Game.Implementations;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZermeloCheckers.GameFactory
{
    public class CheckersFactory
    {
        public IGame CreateGame(GameRequest request)
        {
            request.Validate();

            IPlayer player1 = null;
            IPlayer player2 = null;

            if (request.IsPlayer1ComputerPlayer)
            {
                player1 = new ComputerPlayer(request.Player1Name);
            }
            else
            {
                player1 = new HumanPlayer(request.Player1Name);
            }

            if (request.IsPlayer2ComputerPlayer)
            {
                player2 = new ComputerPlayer(request.Player2Name);
            }
            else
            {
                player2 = new HumanPlayer(request.Player2Name);
            }

            var isRevertedSides = !request.IsPlayer2ComputerPlayer && request.IsPlayer1ComputerPlayer;

            var rules = Checkers.ServiceLocator.CreateRules();
            var game = Game.ServiceLocator.CreateGame(rules, player1, player2, isRevertedSides, request.GameSize);

            return game;
        }
    }
}
