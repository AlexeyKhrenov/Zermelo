using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Implementations
{
    public class Board : IBoard
    {
        public IPlayer Player1 { get; set; }

        public IPlayer Player2 { get; set; }

        public IPlayer ActivePlayer { get; private set; }

        public IPlayer AwaitingPlayer { get; private set; }

        public int Size { get; private set; }

        public bool InvertedCoordinates { get; private set; }

        public IEnumerable<IFigure> GetFigures()
        {
            return Player1.Figures.Union(Player2.Figures);
        }
    }
}
