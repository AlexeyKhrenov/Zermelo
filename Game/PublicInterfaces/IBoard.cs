using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IBoard
    {
        IPlayer ActivePlayer { get; }

        IPlayer AwaitingPlayer { get; }

        int Size { get; }

        bool InvertedCoordinates { get; }

        IEnumerable<IFigure> GetFigures();
    }
}
