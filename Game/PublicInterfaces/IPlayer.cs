using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Game.PublicInterfaces
{
    public interface IPlayer
    {
        string Name { get; }

        /// <summary>
        /// defines behavior of IGame when undoing moves
        /// </summary>
        bool IsComputerPlayer { get; }

        Task MakeMove(IGame game, CancellationToken cancellationToken);

        IEnumerable<IFigure> Figures { get; set; }
    }
}
