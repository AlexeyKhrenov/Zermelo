using System.Collections.Generic;

namespace Game.PublicInterfaces
{
    public interface IBoard
    {
        IPlayer ActivePlayer { get; set; }

        IPlayer AwaitingPlayer { get; set; }

        IPlayer Player1 { get; }

        IPlayer Player2 { get; }

        byte Size { get; }

        IEnumerable<IFigure> Figures { get; }

        void SwitchPlayers();
    }
}