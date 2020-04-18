using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IPlayer
    {
        string Name { get; }

        IList<IFigure> Figures { get; set; }

        void MakeMove(IGame game);
    }
}
