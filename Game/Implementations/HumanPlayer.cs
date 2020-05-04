using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Implementations
{
    public class HumanPlayer : IPlayer
    {
        private string _name;
        public string Name => _name + " (human)";

        public bool IsComputerPlayer => false;

        public IEnumerable<IFigure> Figures { get; set; }

        public bool IsActive { get; set; }

        public HumanPlayer(string name)
        {
            _name = name;
        }

        public Task MakeMove(IGame game, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
