using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game.PublicInterfaces
{
    public interface IPlayer
    {
        string Name { get; }

        void MakeMove(IBoard board, IGame rules, CancellationToken cancellationToken);

        IEnumerable<IFigure> Figures { get; set; }
    }
}
