using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IPlayer
    {
        string Name { get; }

        void MakeMove(IBoard board, IGameRules rules);

        IEnumerable<IFigure> Figures { get; set; }
    }
}
