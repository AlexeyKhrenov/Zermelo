using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game.Implementations
{
    public class HumanPlayer : IPlayer
    {
        private string _name;
        public string Name => _name + " (human)";

        public IEnumerable<IFigure> Figures { get; set; }

        public HumanPlayer(string name)
        {
            _name = name;
        }

        public void MakeMove(IBoard board, IGame rules, CancellationToken cancellationToken)
        {
        }
    }
}
