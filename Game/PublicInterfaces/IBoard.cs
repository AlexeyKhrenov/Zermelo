using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IBoard
    {
        IPlayer ActivePlayer { get; set; }

        IPlayer AwaitingPlayer { get; set; }

        IPlayer Player1 { get; }

        IPlayer Player2 { get; }

        int Size { get; }

        bool InvertedCoordinates { get; }

        IEnumerable<IFigure> Figures { get; }

        void SwitchPlayers();
    }
}
