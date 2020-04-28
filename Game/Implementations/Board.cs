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

        public int Size { get; private set; }

        public bool InvertedCoordinates { get; private set; }

        public Board(IPlayer player1, IPlayer player2, int size, bool invertedCoordinates)
        {
            Size = size;
            InvertedCoordinates = invertedCoordinates;
            Player1 = player1;
            Player2 = player2;

            // by default
            ActivePlayer = player1;
        }

        public IEnumerable<IFigure> GetFigures()
        {
            return Player1.Figures.Union(Player2.Figures);
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
        }
    }
}
