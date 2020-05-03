using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Game.PublicInterfaces
{
    public interface IPlayer
    {
        string Name { get; }

        /// <summary>
        /// defines behavior of IGame when undoing moves
        /// </summary>
        bool IsComputerPlayer { get; }

        void MakeMove(IBoard board, IGame rules, CancellationToken cancellationToken);

        IEnumerable<IFigure> Figures { get; set; }
    }
}
