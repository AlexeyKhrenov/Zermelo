using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Game.PublicInterfaces
{
    public interface IPlayer
    {
        string Name { get; }

        /// <summary>
        ///     defines behavior of IGame when undoing moves
        /// </summary>
        bool IsComputerPlayer { get; }

        bool IsActive { get; set; }

        List<IFigure> Figures { get; set; }

        Task MakeMove(IGame game, CancellationToken cancellationToken);
    }
}