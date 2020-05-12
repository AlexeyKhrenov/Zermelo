using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.PublicInterfaces;

namespace Game.Implementations
{
    public class HumanPlayer : IPlayer
    {
        private readonly string _name;

        public HumanPlayer(string name)
        {
            _name = name;
        }

        public string Name => _name + " (human)";

        public bool IsComputerPlayer => false;

        public List<IFigure> Figures { get; set; }

        public bool IsActive { get; set; }

        public Task MakeMove(IGame game, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}