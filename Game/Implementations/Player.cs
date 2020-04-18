using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementations
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }

        public IList<IFigure> Figures { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}
