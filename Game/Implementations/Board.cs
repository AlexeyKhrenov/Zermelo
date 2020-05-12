using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Implementations
{
    public class Board : IBoard
    {
        public IPlayer Player1 { get; private set; }

        public IPlayer Player2 { get; private set; }

        public IPlayer ActivePlayer { get; set; }

        public IPlayer AwaitingPlayer { get; set; }

        public byte Size { get; private set; }

        public IEnumerable<IFigure> Figures => Player1.Figures.Union(Player2.Figures);

        public Board(IPlayer player1, IPlayer player2, byte size)
        {
            Size = size;
            Player1 = player1;
            Player2 = player2;

            // by default
            ActivePlayer = player1;
            AwaitingPlayer = player2;
        }

        public void SwitchPlayers()
        {
            if (ActivePlayer == Player1)
            {
                ActivePlayer = Player2;
                AwaitingPlayer = Player1;
            }
            else
            {
                ActivePlayer = Player1;
                AwaitingPlayer = Player2;
            }

            ActivePlayer.IsActive = true;
            AwaitingPlayer.IsActive = false;
        }
    }
}
