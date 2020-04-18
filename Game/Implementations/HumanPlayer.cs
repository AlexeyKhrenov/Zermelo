using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementations
{
    public class HumanPlayer : IPlayer
    {
        private string _name;
        public string Name => _name + " (human)";

        public IList<IFigure> Figures { get; set; }

        public HumanPlayer(string name)
        {
            _name = name;
        }

        public void MakeMove(IGame game)
        {
        }
    }
}
